using LifeCounter.Monitor.Hubs;
using LifeCounter.Monitor.Models.Dashboard;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LifeUpdatesHandler
{
    private readonly IHubContext<LifeUpdatesHub> hubContext;
    private readonly DashboardViewModelBuilder dashboardViewModelBuilder;
    private readonly IViewRenderService viewRenderService;


    public LifeUpdatesHandler(
        IHubContext<LifeUpdatesHub> hubContext,
        DashboardViewModelBuilder dashboardViewModelBuilder, IViewRenderService viewRenderService, IHttpContextAccessor contextAccessor)
    {
        this.hubContext = hubContext;
        this.dashboardViewModelBuilder = dashboardViewModelBuilder;
        this.viewRenderService = viewRenderService;
    }

    public async Task HandleAsync(Guid widgetId, string page, string group)
    {
        var viewModel = await dashboardViewModelBuilder.BuildAsync(new DashboardForm()
        {
            WidgetId = widgetId,
            Page = page
        });

        var html = await viewRenderService.RenderToStringAsync("Dashboard/AliveTable", viewModel.Rows);

        await hubContext.Clients
            .Group(group)
            .SendAsync("Update", html);
    }
}