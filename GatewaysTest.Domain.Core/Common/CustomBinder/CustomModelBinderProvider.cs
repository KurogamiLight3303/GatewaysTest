using System.Reflection;
using GatewaysTest.Infrastructure.Extesions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GatewaysTest.Domain.Core.Common.CustomBinder;

public class CustomModelBinderProvider : IModelBinderProvider
{
    private readonly IList<IInputFormatter> _formatters;
    private readonly IHttpRequestStreamReaderFactory _readerFactory;

    public CustomModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
    {
        _formatters = formatters;
        _readerFactory = readerFactory;
    }

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        try
        {
            var customProperties = context.Metadata.ModelType
                .GetProperties()
                .Where(p => p.GetCustomAttribute(typeof(CustomAttributeBinder)) != null)
                .ToArray();
            if (customProperties.Any())
                return new CustomModelBinder(_formatters, _readerFactory, customProperties);
        }
        catch(Exception exc)
        {
            Console.WriteLine($@"Error binging Model: {exc.ToMessageAndCompleteStacktrace()}");
        }
        return null!;
    }
}