using System.Linq.Expressions;

namespace Lab10
{
    public interface IDbRepository<T> where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>> selector);
        IQueryable<T> Get();
        IQueryable<T> GetAll();

        Task<Guid> Add(T newEntity);
        Task AddRange(IEnumerable<T> newEntities);

        Task Delete(int id);

        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);

        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();
    }
}
