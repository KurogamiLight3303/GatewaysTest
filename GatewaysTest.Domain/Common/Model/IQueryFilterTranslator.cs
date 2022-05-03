using System.Linq.Expressions;

namespace GatewaysTest.Domain.Common.Model;

public interface IQueryFilterTranslator<TDomainObject> : IQueryFilterTranslator where TDomainObject : PrimaryDomainEntity
{
    Expression<Func<TDomainObject, bool>>? GetFilter(string alias, object value, 
        QueryFilterType type = QueryFilterType.Equals);
}

public interface IQueryFilterTranslator
{
    
}

public enum QueryFilterType : short
{
    Equals = 0
}