using GatewaysTest.Domain.Core.Common.Queries;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Queries;

public class ListPeripheralsQuery : BaseCollectionQuery<PeripheralResume>
{
    public string? SerialNo { get; init; }
}