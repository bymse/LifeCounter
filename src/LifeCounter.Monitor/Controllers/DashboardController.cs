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
    public async Task<IActionResult> Index(Guid widgetId, [FromQuery] DashboardForm form)
    {
        form.WidgetId = widgetId;
        var viewModel = await dashboardViewModelBuilder.BuildAsync(form);
        // var viewModel = new DashboardIndexViewModel(
        //     new DashboardAppModel(Guid.NewGuid().ToString(), form.Page),
        //     new[]
        //     {
        //         new DashboardRowModel(Guid.NewGuid(), DateTime.Now),
        //         new DashboardRowModel(Guid.NewGuid(), DateTime.Now),
        //         new DashboardRowModel(Guid.NewGuid(), DateTime.Now),
        //     },
        //     form
        // );
        return View(viewModel);
    }
}