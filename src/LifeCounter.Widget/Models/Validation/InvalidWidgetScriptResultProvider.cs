using LifeCounter.Widget.Models.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Models.Validation;

public class InvalidWidgetScriptResultProvider : IInvalidWidgetResultProvider
{
    private readonly WidgetScriptRequestHandler widgetScriptRequestHandler;

    public InvalidWidgetScriptResultProvider(WidgetScriptRequestHandler widgetScriptRequestHandler)
    {
        this.widgetScriptRequestHandler = widgetScriptRequestHandler;
    }

    public IActionResult GetResult(IWidgetIdHolder widgetIdHolder)
        => new JsResult(widgetScriptRequestHandler.GetInvalidWidgetJs(widgetIdHolder));
}