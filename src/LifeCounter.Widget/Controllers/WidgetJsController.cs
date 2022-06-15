using LifeCounter.Widget.Models;
using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Controllers;

public class WidgetJsController : Controller
{
    private readonly WidgetJsRequestHandler widgetJsRequestHandler;

    public WidgetJsController(WidgetJsRequestHandler widgetJsRequestHandler)
    {
        this.widgetJsRequestHandler = widgetJsRequestHandler;
    }

    [Route("/widget/script.js")]
    public IActionResult GetWidget([FromQuery] WidgetScriptRequest request)
    {
        var js = widgetJsRequestHandler.GetWidgetJs(request.WidgetId);
        return new ContentResult
        {
            Content = js,
            ContentType = "application/javascript" 
        };
    }
}