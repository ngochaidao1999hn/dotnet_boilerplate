using Domain.Interfaces;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IAggregateRoot;

        Task<int> CommitTransactionAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null);

        Task RollBackTransactionAsync();
    }
}