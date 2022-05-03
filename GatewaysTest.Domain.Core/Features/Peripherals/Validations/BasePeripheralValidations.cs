using FluentValidation;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Core.Features.Peripherals.Commands;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Validations;

public class BasePeripheralValidations<TCommand> : AbstractValidator<TCommand>
    where TCommand : PeripheralBaseAddOrUpdateCommand
{
    protected IRuleBuilderOptions<TCommand, string?> ConfigureVendor() =>
        RuleFor(p => p.Vendor)
            .MaximumLength(250)
            .NotEmpty()
            .NotNull()
            .WithMessage(I18n.InvalidVendor);
    protected IRuleBuilderOptions<TCommand, string?> ConfigureFabricationDate() =>
        RuleFor(p => p.FabricationDate)
            .Must((fd) => DateTime.TryParse(fd, out var date))
            .NotEmpty()
            .NotNull()
            .WithMessage(I18n.InvalidVendor);
}