using System.Reflection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace GatewaysTest.Domain.Core.Common.CustomBinder;

public class CustomModelBinder : IModelBinder
{
    private readonly BodyModelBinder defaultBinder;
    private readonly PropertyInfo[] customProperties;

    public CustomModelBinder(
        IList<IInputFormatter> formatters, 
        IHttpRequestStreamReaderFactory readerFactory, 
        PropertyInfo[] customProp)
    {
        customProperties = customProp;
        defaultBinder = new(formatters, readerFactory);
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        await defaultBinder.BindModelAsync(bindingContext);

        if (bindingContext.Result.IsModelSet)
        {
            var data = bindingContext.Result.Model;
            if (data != null)
            {
                foreach (var property in customProperties)
                {
                    var value = bindingContext.ValueProvider.GetValue(property.Name).FirstValue;
                    if (value != null)
                        property.SetValue(data, value);
                }
                bindingContext.Result = ModelBindingResult.Success(data);
            }
        }

    }
}