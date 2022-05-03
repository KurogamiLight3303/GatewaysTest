using System.Reflection;
using FluentValidation;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Infrastructure.Extesions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GatewaysTest.Domain.Core.Common;

public static class Configuration
{
    public static IServiceCollection ConfigureMediator(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipeline<,>))
            ;
    }

    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddAutoMapper((requestServices, action) =>
        {
            action.ConstructServicesUsing(requestServices.GetService);
        }, new[] { Assembly.GetExecutingAssembly() }, ServiceLifetime.Scoped);
    }

    public static IActionResult InvalidModelHandling(ActionContext context)
    {
        IEnumerable<string>? errors = null;
        try
        {
            errors = context
                .ModelState
                .Values
                .SelectMany(p => p.Errors)
                .Select(p => p.ErrorMessage);
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToMessageAndCompleteStacktrace());
        }
        return new OkObjectResult(
            new OperationResultValue(false, errors?.FirstOrDefault()?? "Invalid Model"));
    }
}