using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly Dictionary<string, object> repositories = new Dictionary<string, object>();
        private BoilerPlateDbContext _context { get; set; }

        public UnitOfWork(BoilerPlateDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitTransactionAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public IRepository<T> GetRepository<T>() where T : class, IAggregateRoot
        {
            string typeName = typeof(T).Name;
            if (repositories.Keys.Contains(typeName))
            {
                return repositories[typeName] as IRepository<T>;
            }
            IRepository<T> newRepository = new Repository<T>(_context);

            repositories.Add(typeName, newRepository);
            return newRepository;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}