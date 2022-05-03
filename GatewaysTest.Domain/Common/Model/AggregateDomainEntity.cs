namespace GatewaysTest.Domain.Common.Model;

public class AggregateDomainEntity<TParentKey, TChildrenKey, TChildren> : PrimaryDomainEntity<TParentKey>
    where TChildren : SecondaryDomainEntity<TChildrenKey, TParentKey>
{
    public ICollection<TChildren>? Items { get; private set; } = new List<TChildren>();
}