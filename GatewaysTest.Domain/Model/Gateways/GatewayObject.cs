using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Model.Gateways;

public class GatewayObject : AggregateDomainEntity<Guid, Guid, PeripheralObject>
{
    public string? SerialNo { get; set; }
    public string? Name { get; set; }
    public V4IpAddress? IpAddress { get; set; }
}