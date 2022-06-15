using LifeCounter.Widget.Models;
using LifeCounter.Widget.Models.Dto;
using LifeCounter.Widget.Models.Handlers;
using LifeCounter.Widget.Models.Validation;
using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Controllers;

public class WidgetScriptController : Controller
{
    private readonly WidgetScriptRequestHandler widgetScriptRequestHandler;

    public WidgetScriptController(WidgetScriptRequestHandler widgetScriptRequestHandler)
    {
        this.widgetScriptRequestHandler = widgetScriptRequestHandler;
    }

    [Route("/widget/script.js")]
    [WidgetValidationFilter(typeof(InvalidWidgetScriptResultProvider))]
    public IActionResult GetWidget([FromQuery] WidgetScriptRequest request)
    {
        var js = widgetScriptRequestHandler.GetWidgetJs(request);
        return new ContentResult
        {
            Content = js,
            ContentType = "application/javascript" 
        };
    }
}