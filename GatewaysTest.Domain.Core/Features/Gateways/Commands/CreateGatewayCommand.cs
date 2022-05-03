using System.ComponentModel;

namespace GatewaysTest.Domain.Core.Features.Gateways.Commands;

public class CreateGatewayCommand : GatewayBaseAddOrUpdateCommand
{
    [DefaultValue("")]
    public string? SerialNo { get; init; }
}