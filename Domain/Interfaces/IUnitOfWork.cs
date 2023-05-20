using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot;

        Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null);

        Task RollBackTransactionAsync();
    }
}