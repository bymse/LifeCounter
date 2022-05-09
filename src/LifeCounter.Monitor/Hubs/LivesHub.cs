using LifeCounter.Monitor.Models.LifeUpdates;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Hubs;

public class LivesHub : Hub
{
    private readonly ILifeUpdatesSubscriber lifeUpdatesSubscriber;

    public LivesHub(ILifeUpdatesSubscriber lifeUpdatesSubscriber)
    {
        this.lifeUpdatesSubscriber = lifeUpdatesSubscriber;
    }

    public Task Start(Guid widgetId, string page)
    {
        return lifeUpdatesSubscriber.SubscribeAsync(widgetId, page, Context.ConnectionId);
    }
}