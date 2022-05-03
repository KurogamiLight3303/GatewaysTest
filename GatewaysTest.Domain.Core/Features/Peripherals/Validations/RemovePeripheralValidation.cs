using FluentValidation;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Core.Features.Peripherals.Commands;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Validations;

public class RemovePeripheralValidation : AbstractValidator<RemovePeripheralCommand>
{
    public RemovePeripheralValidation()
    {
        RuleFor(p => p.SerialNo)
            .NotEmpty()
            .WithMessage(I18n.InvalidSerialNo);
        RuleFor(p => p.Uid)
            .NotEmpty()
            .WithMessage(I18n.InvalidUid);
    }
}