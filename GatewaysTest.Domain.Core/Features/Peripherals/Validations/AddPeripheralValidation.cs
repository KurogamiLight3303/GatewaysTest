using FluentValidation;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Core.Features.Peripherals.Commands;
using GatewaysTest.Domain.Repositories;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Validations;

public class AddPeripheralValidation : BasePeripheralValidations<AddPeripheralCommand>
{
    private readonly IGatewayQueryRepository _repository;
    public AddPeripheralValidation(IGatewayQueryRepository repository)
    {
        _repository = repository;
        RuleFor(p => p.SerialNo)
            .NotEmpty()
            .WithMessage(I18n.InvalidSerialNo)
            .MustAsync(async (s, cc)
                => await repository.AnyAsync(p => p.SerialNo == s && p.Items!.Count < 10, cc))
            .WithMessage(I18n.OnlyTenPeripheralsByGateway);
        ConfigureVendor();
        ConfigureFabricationDate();
        RuleFor(p => p.SerialNo).NotNull();
        RuleFor(p => p.Uid).MustAsync(async (uid, cc)
            => !await repository.ExistPeripheralAsync(uid, cc))
            .WithMessage(I18n.PeripheralAlreadyExits);
    }
}