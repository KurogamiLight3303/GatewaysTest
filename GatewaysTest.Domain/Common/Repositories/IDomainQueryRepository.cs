using System.Linq.Expressions;
using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Common.Repositories;

public interface IDomainQueryRepository<TDomainObject> : IDomainQueryRepository<TDomainObject, Guid>
    where TDomainObject : PrimaryDomainEntity
{
    
}

public interface IDomainQueryRepository<TDomainObject, TKey> : IDomainRepository<TDomainObject> 
    where TDomainObject : PrimaryDomainEntity<TKey>
{
    Task<PagedResultValue<TProjectedValue>> SearchAsync<TProjectedValue>(PagedRequestValue parameters,
        CancellationToken cancellationToken = default) where TProjectedValue : DomainResumeObject<TDomainObject, TKey>;
    Task<PagedResultValue<TDomainObject>> SearchAsync(PagedRequestValue parameters,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TDomainObject, bool>> condition, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<TDomainObject, bool>> condition, CancellationToken cancellationToken = default);
}