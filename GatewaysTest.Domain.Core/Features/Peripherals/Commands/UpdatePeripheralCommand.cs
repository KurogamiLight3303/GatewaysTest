using System.ComponentModel;
using System.Text.Json.Serialization;
using HybridModelBinding;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Commands;

[HybridBindClass(new []{Source.Body, Source.Route})]
public class UpdatePeripheralCommand : PeripheralBaseAddOrUpdateCommand
{
    [JsonIgnore]
    [DefaultValue("")]
    [HybridBindProperty(Source.Route, "uid")]
    public int Uid { get; set; }
}