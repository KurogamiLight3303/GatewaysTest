namespace GatewaysTest.Domain.Model.Gateways;

public class GatewayDetailResume : GatewayResume
{
    public PeripheralResume[]? Peripherals { get; set; }
}