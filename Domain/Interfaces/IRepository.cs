using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class, IAggregateRoot
    {
        Task Create(T entity);

        void Update(T entity);

        Task<T> GetById(int id);

        void Delete(int id);

        Task<IQueryable<T>> Get(Expression<Func<T, bool>> filter = null, string[] includeProperties = null);
    }
}