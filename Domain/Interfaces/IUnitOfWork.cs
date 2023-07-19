using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Product> productRepository { get; }
        Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null);

        Task RollBackTransactionAsync();
    }
}