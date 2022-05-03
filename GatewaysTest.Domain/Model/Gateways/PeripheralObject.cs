using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Model.Gateways;

public class PeripheralObject : SecondaryDomainEntity<Guid, Guid>
{
    public int UID { get; set; }
    public string? Vendor { get; set; }
    public bool Status { get; set; }
    public DateTime FabricationDate { get; set; }
}