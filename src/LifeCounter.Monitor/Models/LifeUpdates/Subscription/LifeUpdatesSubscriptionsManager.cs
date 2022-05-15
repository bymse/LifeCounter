using System.Collections.Concurrent;
using LifeCounter.Common.Container;
using LifeCounter.Common.Store;
using LifeCounter.Common.Utilities.Lock;
using StackExchange.Redis;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

[PreventAutoRegistration]
public class LifeUpdatesSubscriptionsManager : ILifeUpdatesSubscriptionsManager
{
    private static readonly NamedLock GroupLock = new();

    private static readonly ConcurrentDictionary<string, LifeUpdatesSubscription> Groups = new();
    private static readonly ConcurrentDictionary<string, ISet<LifeUpdatesIdentifier>> Connections = new();

    private readonly ILifeStore lifeStore;

    public LifeUpdatesSubscriptionsManager(ILifeStore lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    public void Subscribe(LifeUpdatesSubscribeRequest request, OnMessage onMessage)
    {
        var (connectionId, identifier) = request;
        var key = identifier.GetKey();
        using (GroupLock.Lock(key))
        {
            if (Groups.TryGetValue(key, out var subscription))
            {
                subscription.Connections.Add(connectionId);
            }
            else
            {
                var queue = lifeStore.Subscribe(identifier.WidgetId, identifier.Page);
                queue.OnMessage(GetOnMessage(onMessage, identifier));
                subscription = Groups[key] = new LifeUpdatesSubscription(queue);
                subscription.Connections.Add(connectionId);
            }
        }

        Connections.AddOrUpdate(connectionId, new HashSet<LifeUpdatesIdentifier>()
        {
            identifier
        }, (_, list) => new HashSet<LifeUpdatesIdentifier>(list)
        {
            identifier
        });
    }

    private static Func<ChannelMessage, Task> GetOnMessage(
        OnMessage onMessageExternal,
        LifeUpdatesIdentifier identifier
    )
    {
        var key = identifier.GetKey();

        return async _ =>
        {
            string[] connections;
            using (await GroupLock.LockAsync(key))
            {
                if (Groups.TryGetValue(key, out var subscription))
                    connections = subscription.Connections.ToArray();
                else
                    return;
            }

            await onMessageExternal(identifier, connections);
        };
    }

    public void Unsubscribe(string connectionId)
    {
        if (!Connections.TryRemove(connectionId, out var identifiers)) return;

        foreach (var identifier in identifiers)
        {
            Unsubscribe(connectionId, identifier);
        }
    }

    private static void Unsubscribe(string connectionId, LifeUpdatesIdentifier identifier)
    {
        var key = identifier.GetKey();
        using (GroupLock.Lock(key))
        {
            if (!Groups.TryGetValue(key, out var subscription)) return;
            
            Groups.TryRemove(key, out _);

            subscription.Connections.Remove(connectionId);
            if (subscription.Connections.Count == 0) 
                subscription.Queue.Unsubscribe();
        }
    }
}