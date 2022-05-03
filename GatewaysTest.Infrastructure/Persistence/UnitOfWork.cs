using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using GatewaysTest.Domain.Common.Repositories;

namespace GatewaysTest.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DomainContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(DomainContext context)
    {
        _context = context;
    }

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<CancellationToken, Task<TResult>> operation, 
        CancellationToken cancellationToken)
    {
        if (_transaction != null)
            throw new InvalidOperationException("Already in transaction");
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return await _context.Database.CreateExecutionStrategy().ExecuteInTransactionAsync(operation, 
            token => Task.FromResult(true), IsolationLevel.Serializable,cancellationToken);
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        var result = false;
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            if (_transaction != null)
                await _transaction.CommitAsync(cancellationToken);
            result = true;
        }
        catch
        {
            
        }

        return result;
    }
}