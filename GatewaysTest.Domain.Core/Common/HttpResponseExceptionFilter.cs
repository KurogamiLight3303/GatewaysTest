using FluentValidation;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Infrastructure.Extesions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GatewaysTest.Domain.Core.Common;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ValidationException exception)
        {
            context.Result = new ObjectResult
                (new OperationResultValue(false, exception.Errors.FirstOrDefault()?.ErrorMessage 
                                                 ?? I18n.UnknowError));
            context.ExceptionHandled = true;
        }
        else if (context.Exception != null)
        {
            Console.WriteLine(context.Exception.ToMessageAndCompleteStacktrace());
            context.Result = new ObjectResult(new OperationResultValue(false, I18n.UnknowError));
            context.ExceptionHandled = true;
        }
    }
}