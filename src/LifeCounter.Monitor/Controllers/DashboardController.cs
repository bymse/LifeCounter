using LifeCounter.Monitor.Models;
using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Monitor.Controllers;

public class DashboardController : Controller
{
    private readonly DashboardViewModelBuilder dashboardViewModelBuilder;

    public DashboardController(DashboardViewModelBuilder dashboardViewModelBuilder)
    {
        this.dashboardViewModelBuilder = dashboardViewModelBuilder;
    }

    [Route("/monitor/dashboard/{widgetId:guid}")]
    public async Task<IActionResult> Index(Guid widgetId, string page)
    {
        var viewModel = await dashboardViewModelBuilder.BuildAsync(widgetId, page);
        return View(viewModel);
    }
}