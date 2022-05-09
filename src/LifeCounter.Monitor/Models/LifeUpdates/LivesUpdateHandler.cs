using LifeCounter.Monitor.Hubs;
using LifeCounter.Monitor.Models.Dashboard;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LivesUpdateHandler
{
    private readonly IHubContext<LivesHub> hubContext;
    private readonly DashboardViewModelBuilder dashboardViewModelBuilder;
    private readonly IViewRenderService viewRenderService;


    public LivesUpdateHandler(
        IHubContext<LivesHub> hubContext,
        DashboardViewModelBuilder dashboardViewModelBuilder, IViewRenderService viewRenderService, IHttpContextAccessor contextAccessor)
    {
        this.hubContext = hubContext;
        this.dashboardViewModelBuilder = dashboardViewModelBuilder;
        this.viewRenderService = viewRenderService;
    }

    public async Task HandleAsync(IReadOnlyList<string> connections, Guid widgetId, string page)
    {
        var viewModel = await dashboardViewModelBuilder.BuildAsync(new DashboardForm()
        {
            WidgetId = widgetId,
            Page = page
        });

        var html = await viewRenderService.RenderToStringAsync("Dashboard/AliveTable", viewModel.Rows);

        await hubContext.Clients
            .Clients(connections)
            .SendAsync("Update", html);
    }
}