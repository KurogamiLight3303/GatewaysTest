using FluentValidation;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Core.Features.Gateways.Commands;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Core.Features.Gateways.Validations;

public class BaseGatewayValidations<TCommand> : AbstractValidator<TCommand> 
    where TCommand : GatewayBaseAddOrUpdateCommand
{
    protected IRuleBuilderOptions<TCommand, string?> ConfigureIpAddress()
    {
        return RuleFor(p => p.IpAddress)
            .Must(V4IpAddress.IsValid)
            .WithMessage(I18n.InvalidIpAddress);
    }

    protected IRuleBuilderOptions<TCommand, string> ConfigureName()
    {
        return RuleFor(p => p.Name)
            .MaximumLength(250)
            .NotEmpty()
            .WithMessage(I18n.InvalidName);
    }
}