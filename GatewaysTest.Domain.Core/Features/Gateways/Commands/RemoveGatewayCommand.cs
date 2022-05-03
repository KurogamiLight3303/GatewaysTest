using GatewaysTest.Domain.Core.Common.Commands;

namespace GatewaysTest.Domain.Core.Features.Gateways.Commands;

public class RemoveGatewayCommand : CommandBase
{
    public string SerialNo { get; init; }
}