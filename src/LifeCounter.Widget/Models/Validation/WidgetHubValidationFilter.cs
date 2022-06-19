using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Widget.Models.Validation;

public class WidgetHubValidationFilter : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        var widgetIdHolder = invocationContext.HubMethodArguments
            .OfType<IWidgetIdHolder>()
            .FirstOrDefault();

        if (widgetIdHolder != null)
        {
            var requestValidator = invocationContext.ServiceProvider.GetRequiredService<WidgetRequestValidator>();
            var isValid = requestValidator.IsValid(widgetIdHolder);

            if (!isValid)
            {
                invocationContext.Context.Abort();
                return null;
            }
        }

        return await next(invocationContext);
    }
}