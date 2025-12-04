using Microsoft.EntityFrameworkCore.Storage;

namespace EventFlow.Shared.UnitOfWork;

public interface IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    IExecutionStrategy CreateExecutionStrategy();
}