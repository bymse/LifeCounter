using StackExchange.Redis;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSubscription
{
    public LifeUpdatesSubscription(ChannelMessageQueue queue)
    {
        Queue = queue;
        Connections = new List<string>();
    }

    public ChannelMessageQueue Queue { get; }
    public IList<string> Connections { get; }
}