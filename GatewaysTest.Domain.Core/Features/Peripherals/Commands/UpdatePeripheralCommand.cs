using System.ComponentModel;
using System.Text.Json.Serialization;
using GatewaysTest.Domain.Core.Common.CustomBinder;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Commands;

public class UpdatePeripheralCommand : PeripheralBaseAddOrUpdateCommand
{
    [JsonIgnore]
    [DefaultValue("")]
    [CustomAttributeBinder]
    public int Uid { get; set; }
}