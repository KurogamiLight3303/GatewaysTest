using FluentValidation;
using GatewaysTest.Domain.Core.Features.Gateways.Commands;

namespace GatewaysTest.Domain.Core.Features.Gateways.Validations;

public class UpdateGatewayValidation : BaseGatewayValidations<UpdateGatewayCommand>
{
    public UpdateGatewayValidation()
    {
        ConfigureIpAddress()
            .When(p => string.IsNullOrEmpty(p.IpAddress));
        ConfigureName()
            .When(p => string.IsNullOrEmpty(p.Name));
    }
}