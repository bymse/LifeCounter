using System.Collections.Concurrent;
using StackExchange.Redis;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSubscription
{
    public LifeUpdatesSubscription(ChannelMessageQueue queue, string firstClient, Guid widgetId, string page)
    {
        Queue = queue;
        WidgetId = widgetId;
        Page = page;
        Clients = new ConcurrentBag<string> {firstClient};
    }

    public ChannelMessageQueue Queue { get; }
    public Guid WidgetId { get; }
    public string Page { get; }
    public ConcurrentBag<string> Clients { get; }
}