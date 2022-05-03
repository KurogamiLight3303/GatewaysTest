using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace GatewaysTest.Domain.Core.Common;

public class FluentValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public FluentValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request
        , CancellationToken cancellationToken
        , RequestHandlerDelegate<TResponse> next
    )
    {
        var failures = new List<ValidationFailure>();
        foreach (var validator in _validators)
        {
            var r = await validator.ValidateAsync(request, cancellationToken);
            if(r?.Errors != null)
                failures.AddRange(r.Errors);
        }

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}