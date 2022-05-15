using LifeCounter.Monitor.Models.LifeUpdates;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Monitor.Hubs;

public class LifeUpdatesHub : Hub
{
    private readonly ILifeUpdatesSubscriber lifeUpdatesSubscriber;
    private readonly IServiceProvider serviceProvider;

    public LifeUpdatesHub(ILifeUpdatesSubscriber lifeUpdatesSubscriber, IServiceProvider serviceProvider)
    {
        this.lifeUpdatesSubscriber = lifeUpdatesSubscriber;
        this.serviceProvider = serviceProvider;
    }

    public Task Start(Guid widgetId, string page)
    {
        return lifeUpdatesSubscriber.SubscribeAsync(widgetId, page, Context.ConnectionId);
    }

    public async Task Refresh(Guid widgetId, string page)
    {
        using var scope = serviceProvider.CreateScope();
        var sender = scope.ServiceProvider.GetService<ILifeUpdatesUpdateSender>()!;
        await sender.SendAsync(new LifeUpdatesIdentifier(widgetId, page), new[]
        {
            Context.ConnectionId
        });
    }
}