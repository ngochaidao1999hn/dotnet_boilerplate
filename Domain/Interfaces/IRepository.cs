using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> CreateAsync(T entity);

        void Update(T entity);

        Task<T> GetByIdAsync(int id);

        void Delete(int id);

        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null);
    }
}