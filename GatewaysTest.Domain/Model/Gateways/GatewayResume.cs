using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Model.Gateways;

public class GatewayResume : DomainResumeObject<GatewayObject>
{
    public string? SerialNo { get; set; }
    public string? Name { get; set; }
    public string? IpAddress { get; set; }
}