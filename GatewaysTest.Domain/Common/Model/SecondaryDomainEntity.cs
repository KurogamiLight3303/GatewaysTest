namespace GatewaysTest.Domain.Common.Model;

public abstract class SecondaryDomainEntity<TKey, TParentKey> : DomainEntity<TKey>
{
    public TParentKey? ParentId { get; set; }
}