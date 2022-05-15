using LifeCounter.Monitor.Hubs;
using LifeCounter.Monitor.Models.Dashboard;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LifeUpdatesNotificationHandler : ILifeUpdatesNotificationHandler
{
    private readonly ILogger<LifeUpdatesNotificationHandler> logger;
    private readonly IHubContext<LifeUpdatesHub> hubContext;
    private readonly DashboardRowsViewModelBuilder rowsViewModelBuilder;
    private readonly IViewRenderService viewRenderService;

    public LifeUpdatesNotificationHandler(
        ILogger<LifeUpdatesNotificationHandler> logger,
        IHubContext<LifeUpdatesHub> hubContext,
        IViewRenderService viewRenderService,
        DashboardRowsViewModelBuilder rowsViewModelBuilder
    )
    {
        this.logger = logger;
        this.hubContext = hubContext;
        this.viewRenderService = viewRenderService;
        this.rowsViewModelBuilder = rowsViewModelBuilder;
    }

    public async Task HandleAsync(LifeUpdatesIdentifier identifier, IReadOnlyList<string> clients)
    {
        try
        {
            var viewModel = await rowsViewModelBuilder.BuildAsync(identifier.WidgetId, identifier.Page);
            var html = await viewRenderService.RenderToStringAsync("Dashboard/AliveTable", viewModel);
            
            await hubContext.Clients
                .Clients(clients)
                .SendAsync("Update", html);
        }
        catch
        {
            logger.LogError("Error while handling update for {identifier}", new { identifier });
        }
    }
}