using LifeCounter.Monitor.Models.LifeUpdates.Subscription;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Hubs;

public class LifeUpdatesHub : Hub
{
    private readonly ILifeUpdatesSubscriber lifeUpdatesSubscriber;

    public LifeUpdatesHub(ILifeUpdatesSubscriber lifeUpdatesSubscriber)
    {
        this.lifeUpdatesSubscriber = lifeUpdatesSubscriber;
    }

    public Task Start(Guid widgetId, string page)
    {
        return lifeUpdatesSubscriber.SubscribeAsync(widgetId, page, Context.ConnectionId);
    }
}