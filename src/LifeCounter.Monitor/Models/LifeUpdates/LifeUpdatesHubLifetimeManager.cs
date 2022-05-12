using LifeCounter.Monitor.Hubs;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LifeUpdatesHubLifetimeManager<THub> : DefaultHubLifetimeManager<LifeUpdatesHub>
{
    private readonly IServiceProvider serviceProvider;

    public LifeUpdatesHubLifetimeManager(
        ILogger<DefaultHubLifetimeManager<LifeUpdatesHub>> logger,
        IServiceProvider serviceProvider
    ) : base(logger)
    {
        this.serviceProvider = serviceProvider;
    }

    public override Task OnDisconnectedAsync(HubConnectionContext connection)
    {
        serviceProvider
            .GetService<LifeUpdatesSubscriptionsManager>()!
            .Unsubscribe(connection.ConnectionId);

        return base.OnDisconnectedAsync(connection);
    }
}