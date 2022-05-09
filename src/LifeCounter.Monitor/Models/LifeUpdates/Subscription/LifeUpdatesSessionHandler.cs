using System.Collections.Concurrent;
using LifeCounter.Common.Store;
using LifeCounter.Monitor.Hubs;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSessionHandler
{
    private static readonly ConcurrentDictionary<string, object> Locks = new();

    private static readonly Dictionary<string, LifeUpdatesSubscription> WidgetsToClients = new();

    private readonly ILifeStore lifeStore;
    private readonly LivesUpdateHandler livesUpdateHandler;
    private readonly IHubContext<LivesHub> hubContext;

    public LifeUpdatesSessionHandler(ILifeStore lifeStore, LivesUpdateHandler livesUpdateHandler)
    {
        this.lifeStore = lifeStore;
        this.livesUpdateHandler = livesUpdateHandler;
    }

    public Task HandleAsync(LifeUpdatesSession session)
    {
        var key = session.GetKey();
        lock (Locks.GetOrAdd(key, () => new object()))
        {
            if (WidgetsToClients.TryGetValue(key, out var subscriptions))
            {
                hubContext.Groups.AddToGroupAsync(session.ClientId, key);
                subscriptions.Clients.Add(session.ClientId);
            }
            else
            {
                var queue = lifeStore.Subscribe(session.WidgetId, session.Page);
                var model = new LifeUpdatesSubscription(queue, session.ClientId, session.WidgetId, session.Page);
                queue.OnMessage(e => HandleMessage(e, model));
                WidgetsToClients.Add(key, model);
            }
        }
        
        return Task.CompletedTask;
    }

    private Task HandleMessage(ChannelMessage channelMessage, LifeUpdatesSubscription lifeUpdatesSubscription)
    {
        return livesUpdateHandler.HandleAsync(
            lifeUpdatesSubscription.Clients.ToArray(),
            lifeUpdatesSubscription.WidgetId,
            lifeUpdatesSubscription.Page
        );
    }
}