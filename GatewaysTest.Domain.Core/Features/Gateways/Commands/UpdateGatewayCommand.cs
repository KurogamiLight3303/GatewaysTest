using System.ComponentModel;
using System.Text.Json.Serialization;
using GatewaysTest.Domain.Core.Common.CustomBinder;

namespace GatewaysTest.Domain.Core.Features.Gateways.Commands;

public class UpdateGatewayCommand : GatewayBaseAddOrUpdateCommand
{
    [JsonIgnore]
    [DefaultValue("")]
    [CustomAttributeBinder]
    public string? SerialNo { get; init; }
}