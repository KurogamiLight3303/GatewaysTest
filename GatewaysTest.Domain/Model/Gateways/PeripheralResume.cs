using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Model.Gateways;

public class PeripheralResume : DomainResumeObject<PeripheralObject>
{
    public int Uid { get; set; }
    public string? Vendor { get; set; }
    public bool Status { get; set; }
    public string? FabricationDate { get; set; }
}