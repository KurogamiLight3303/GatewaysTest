using GatewaysTest.Domain.Core.Common.CustomBinder;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Commands;

public class AddPeripheralCommand : PeripheralBaseAddOrUpdateCommand
{
    [CustomAttributeBinder]
    public int Uid { get; init; }
}