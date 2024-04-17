using Lab10;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<Lab10.AppDbContext, Lab10.DataContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=lab10;Username=postgres;Password=1111;"));
// Register IDbRepository<Film> and its implementation
builder.Services.AddScoped<IDbRepository<Film>, FilmRepository<Film>>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// EnsureCreated для создания базы данных при запуске приложения
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Lab10.DataContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();