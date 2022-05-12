using System.Collections.Concurrent;
using LifeCounter.Common.Store;
using StackExchange.Redis;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSubscriptionsManager
{
    private static readonly ConcurrentDictionary<string, object> Locks = new();

    private static readonly Dictionary<string, ChannelMessageQueue> GroupsToQueues = new();

    private readonly ILifeStore lifeStore;
    private readonly LifeUpdatesHandler lifeUpdatesHandler;
    private readonly ILifeUpdatesGroupsManager groupsManager;

    public LifeUpdatesSubscriptionsManager(
        ILifeStore lifeStore,
        LifeUpdatesHandler lifeUpdatesHandler,
        ILifeUpdatesGroupsManager groupsManager
    )
    {
        this.lifeStore = lifeStore;
        this.lifeUpdatesHandler = lifeUpdatesHandler;
        this.groupsManager = groupsManager;
    }

    public Task SubscribeAsync(LifeUpdatesSession session)
    {
        var key = session.GetKey();
        lock (Locks.GetOrAdd(key, () => new object()))
        {
            if (!groupsManager.GroupsExists(key))
            {
                var queue = lifeStore.Subscribe(session.WidgetId, session.Page);
                var model = new LifeUpdatesSubscription(session.WidgetId, session.Page);
                queue.OnMessage(e => HandleMessage(model, key));
                GroupsToQueues.Add(key, queue);
            }

            groupsManager.AddToGroupAsync(key, session.ClientId);
        }

        return Task.CompletedTask;
    }

    public void Unsubscribe(string connectionId)
    {
        var groups = groupsManager.RemoveFromGroupsAndGetNames(connectionId);
        foreach (var group in groups)
        {
            lock (Locks.GetOrAdd(group, () => new object()))
            {
                if (!groupsManager.GroupsExists(group))
                {
                    GroupsToQueues[group].Unsubscribe();
                }
            }
        }
    }

    private Task HandleMessage(LifeUpdatesSubscription lifeUpdatesSubscription, string group)
    {
        return lifeUpdatesHandler.HandleAsync(
            lifeUpdatesSubscription.WidgetId,
            lifeUpdatesSubscription.Page,
            group
        );
    }
}