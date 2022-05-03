using GatewaysTest.Domain.Core.Common.Queries;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Core.Features.Gateways.Queries;

public class GetGatewayQuery : BaseQuery<GatewayDetailResume>
{
    public string? SerialNo { get; init; }
}