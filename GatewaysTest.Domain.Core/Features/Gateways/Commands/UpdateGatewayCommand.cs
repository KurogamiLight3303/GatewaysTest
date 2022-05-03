using System.ComponentModel;
using System.Text.Json.Serialization;
using HybridModelBinding;

namespace GatewaysTest.Domain.Core.Features.Gateways.Commands;

[HybridBindClass(new []{Source.Body, Source.Route})]
public class UpdateGatewayCommand : GatewayBaseAddOrUpdateCommand
{
    [JsonIgnore]
    [DefaultValue("")]
    [HybridBindProperty(Source.Route)]
    public string? SerialNo { get; init; }
}