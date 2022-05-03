using System.Linq.Expressions;
using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Common.Repositories;

public interface IDomainCommandRepository<TDomainObject, TKey> : IDomainRepository <TDomainObject>
    where TDomainObject : PrimaryDomainEntity<TKey>
{
    Task AddAsync(TDomainObject entity, CancellationToken cancellationToken = default);
    void Update(TDomainObject entity);
    bool Update(TDomainObject entity, Dictionary<Expression<Func<TDomainObject, object>>, object> changes);
    void Remove(TDomainObject entity);
}

public interface IDomainCommandRepository<TDomainObject> : IDomainCommandRepository <TDomainObject, Guid>
    where TDomainObject : PrimaryDomainEntity<Guid>
{
}