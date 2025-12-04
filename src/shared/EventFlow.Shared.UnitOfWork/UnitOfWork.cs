using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EventFlow.Shared.UnitOfWork;

public class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork
    where TContext : DbContext
{
    private IDbContextTransaction? _currentTransaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _currentTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return dbContext.Database.CreateExecutionStrategy();
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            await _currentTransaction!.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            _currentTransaction!.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        await _currentTransaction!.RollbackAsync(cancellationToken);
        _currentTransaction.Dispose();
        _currentTransaction = null;
    }
}