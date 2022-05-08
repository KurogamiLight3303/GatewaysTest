using System.ComponentModel;
using System.Text.Json.Serialization;
using GatewaysTest.Domain.Core.Common.Commands;
using GatewaysTest.Domain.Core.Common.CustomBinder;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Commands;

public abstract class PeripheralBaseAddOrUpdateCommand : CommandBase<PeripheralResume>
{
    [DefaultValue("2000-01-01")]
    public string? FabricationDate { get; init; }
    [DefaultValue("")]
    public string? Vendor { get; init; }
    [DefaultValue(false)]
    public bool Status { get; init; }
    [JsonIgnore]
    [DefaultValue("")]
    [CustomAttributeBinder]
    public string? SerialNo { get; set; }
}