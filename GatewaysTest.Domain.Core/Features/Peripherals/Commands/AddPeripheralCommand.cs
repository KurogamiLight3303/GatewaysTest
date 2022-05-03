using HybridModelBinding;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Commands;

[HybridBindClass(new []{Source.Body, Source.Route})]
public class AddPeripheralCommand : PeripheralBaseAddOrUpdateCommand
{
    public int Uid { get; init; }
}