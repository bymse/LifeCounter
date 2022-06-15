using LifeCounter.Widget.Models;
using LifeCounter.Widget.Models.Dto;
using LifeCounter.Widget.Models.Handlers;
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