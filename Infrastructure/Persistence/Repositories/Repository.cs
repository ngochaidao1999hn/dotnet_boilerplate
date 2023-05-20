using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected BoilerPlateDbContext _context { get; set; }
        protected DbSet<T> dbSet;

        public Repository(BoilerPlateDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var res =  await dbSet.AddAsync(entity);
            return res.Entity;
        }

        public void Delete(int id)
        {
            T entityToDelete = dbSet.Find(id);
            _context.Remove(entityToDelete);
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            var data = await query.ToListAsync();
            return data.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}