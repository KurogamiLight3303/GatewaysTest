using System.Reflection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace GatewaysTest.Domain.Core.Common.CustomBinder;

public class CustomModelBinderProvider : IModelBinderProvider
{
    private readonly IList<IInputFormatter> formatters;
    private readonly IHttpRequestStreamReaderFactory readerFactory;

    public CustomModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
    {
        this.formatters = formatters;
        this.readerFactory = readerFactory;
    }

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        try
        {
            var customProperties = context.Metadata.ModelType
                .GetProperties().Where(p => p.GetCustomAttribute(typeof(CustomAttributeBinder)) != null);
            if (customProperties.Any())
                return new CustomModelBinder(formatters, readerFactory, customProperties.ToArray());
        }
        catch
        {
            
        }
        return null;
    }
}