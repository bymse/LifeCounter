using LifeCounter.Monitor.Hubs;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LifeUpdatesHubLifeTimeManager<T> : DefaultHubLifetimeManager<T> where T : Hub
{
    private readonly ILifeUpdatesSubscriptionsManager subscriptionsManager;

    public LifeUpdatesHubLifeTimeManager(
        ILogger<LifeUpdatesHubLifeTimeManager<T>> logger,
        ILifeUpdatesSubscriptionsManager subscriptionsManager
    ) : base(logger)
    {
        this.subscriptionsManager = subscriptionsManager;
    }

    public override Task OnDisconnectedAsync(HubConnectionContext connection)
    {
        if (typeof(T) == typeof(LifeUpdatesHub))
        {
            subscriptionsManager.Unsubscribe(connection.ConnectionId);
        }

        return base.OnDisconnectedAsync(connection);
    }
}