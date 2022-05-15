namespace GatewaysTest.Domain.Common.Model;

public interface IQueryFilterTranslator<TDomainObject, TKey> : IQueryFilterTranslator 
    where TDomainObject : PrimaryDomainEntity<TKey>
{
    Task<IQueryable<TDomainObject>> AddFiltersAsync(IQueryable<TDomainObject> query, 
        IEnumerable<QueryFilterValue> filters, CancellationToken cancellationToken);
}

public interface IQueryFilterTranslator
{
    
}