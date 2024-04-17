using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly IDbRepository<Film> _repository;

        public FilmController(IDbRepository<Film> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Film> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetById(int id)
        {
            var film = await _repository.Get(x => x.Id == id).FirstOrDefaultAsync();
            if (film == null)
            {
                return NotFound();
            }
            return film;
        }

        [HttpPost]
        public async Task<ActionResult<Film>> Create(Film film)
        {
            await _repository.Add(film);
            return CreatedAtAction(nameof(GetById), new { id = film.Id }, film);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Film updatedFilm)
        {
            if (id != updatedFilm.Id)
            {
                return BadRequest("The ID in the URL path does not match the ID in the request body.");
            }

            var existingFilm = await _repository.Get(x => x.Id == id).FirstOrDefaultAsync();
            if (existingFilm == null)
            {
                return NotFound();
            }

            existingFilm.Title = updatedFilm.Title;
            existingFilm.Director = updatedFilm.Director;
            existingFilm.Writer = updatedFilm.Writer;
            existingFilm.Genre = updatedFilm.Genre;
            existingFilm.Year = updatedFilm.Year;

            await _repository.Update(existingFilm);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var film = await _repository.Get(x => x.Id == id).FirstOrDefaultAsync();
            if (film == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }
    }
}
