namespace GatewaysTest.Domain.Common.Model;

public abstract  class DomainEntity<TKey>
{
    public TKey? Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}