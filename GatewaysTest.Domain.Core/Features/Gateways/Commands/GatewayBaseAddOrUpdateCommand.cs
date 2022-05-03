using System.ComponentModel;
using GatewaysTest.Domain.Core.Common.Commands;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Core.Features.Gateways.Commands;

public abstract class GatewayBaseAddOrUpdateCommand : CommandBase<GatewayResume>
{
    [DefaultValue("")]
    public string? Name { get; init; }
    [DefaultValue("")]
    public string? IpAddress { get; init; }
}