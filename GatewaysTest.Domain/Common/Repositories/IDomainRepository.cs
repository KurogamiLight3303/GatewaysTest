using System.Linq.Expressions;

namespace GatewaysTest.Domain.Common.Repositories;

public interface IDomainRepository<TDomainObject> : IDomainRepository
{
    Task<TDomainObject?> FindAsync(Expression<Func<TDomainObject, bool>> condition, 
        CancellationToken cancellationToken = default);

    Task<TDomainObject?> FindAsync<TKey>(Expression<Func<TDomainObject, bool>> condition,
        Expression<Func<TDomainObject, TKey>> orderBy, CancellationToken cancellationToken = default);
    Task<TDomainObject?> FindAsync(Expression<Func<TDomainObject, bool>> condition,
        IEnumerable<Expression<Func<TDomainObject, object>>> includes,
        CancellationToken cancellationToken = default);

    Task<TDomainObject?> FindLastAsync(Expression<Func<TDomainObject, bool>> condition,
        CancellationToken cancellationToken = default);

    Task<TDomainObject?> FindLastAsync<TKey>(Expression<Func<TDomainObject, bool>> condition,
        Expression<Func<TDomainObject, TKey>> orderBy, CancellationToken cancellationToken = default);
}

public interface IDomainRepository
{
    
}