using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Monitor.Controllers;

public class DashboardController : Controller
{
    [Route("/monitor/dashboard/{widgetId:guid}")]
    public IActionResult Index(Guid widgetId)
    {
        return View();
    }
}