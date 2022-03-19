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

    [Route("/widget")]
    public IActionResult GetWidget(Guid widgetId)
    {
        var js = widgetJsRequestHandler.GetWidgetJs(widgetId);
        return new ContentResult
        {
            Content = js,
            ContentType = "application/javascript" 
        };
    }
}