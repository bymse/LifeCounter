using LifeCounter.Monitor.Models;
using LifeCounter.Monitor.Models.Dashboard;
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
    public async Task<IActionResult> Index([FromRoute] [FromQuery] DashboardForm form)
    {
        var viewModel = await dashboardViewModelBuilder.BuildAsync(form);
        return View(viewModel);
    }
}