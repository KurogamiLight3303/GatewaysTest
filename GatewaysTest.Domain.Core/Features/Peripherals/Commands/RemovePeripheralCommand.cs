using GatewaysTest.Domain.Core.Common.Commands;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Commands;

public class RemovePeripheralCommand : CommandBase
{
    public string? SerialNo { get; init; }
    public int Uid { get; init; }
}