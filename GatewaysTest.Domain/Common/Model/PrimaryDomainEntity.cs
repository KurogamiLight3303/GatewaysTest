namespace GatewaysTest.Domain.Common.Model;

public abstract  class PrimaryDomainEntity<TKey> : DomainEntity<TKey>
{
    
}

public abstract class PrimaryDomainEntity : PrimaryDomainEntity<Guid>
{
    
}

