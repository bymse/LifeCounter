using Microsoft.AspNetCore.Mvc.Filters;

namespace LifeCounter.Widget.Models.Validation;

public class WidgetValidationFilter<T> : IActionFilter where T : IInvalidWidgetResultProvider 
{
    private readonly WidgetRequestValidator validator;

    public WidgetValidationFilter(WidgetRequestValidator validator)
    {
        this.validator = validator;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var widgetIdHolder = context.ActionArguments.Values.OfType<IWidgetIdHolder>().FirstOrDefault();
        if (widgetIdHolder == null)
        {
            return;
        }

        if (!validator.IsValid(widgetIdHolder))
        {
            var resultProvider = context.HttpContext.RequestServices.GetRequiredService<T>();
            context.Result = resultProvider.GetResult(widgetIdHolder);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}