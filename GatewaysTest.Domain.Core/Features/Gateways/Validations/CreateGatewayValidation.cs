using FluentValidation;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Core.Features.Gateways.Commands;
using GatewaysTest.Domain.Repositories;

namespace GatewaysTest.Domain.Core.Features.Gateways.Validations;

public class CreateGatewayValidation : BaseGatewayValidations<CreateGatewayCommand>
{
    private readonly IGatewayQueryRepository _repository;
    public CreateGatewayValidation(IGatewayQueryRepository repository)
    {
        _repository = repository;
        RuleFor(p => p.SerialNo)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage(I18n.InvalidSerialNo)
            .MustAsync(async (s, cc)
                => !await _repository.AnyAsync(p => p.SerialNo == s, cc))
            .WithMessage(I18n.SerialNoAlreadyExist)
            ;
        ConfigureName();
        ConfigureIpAddress();
    }
}