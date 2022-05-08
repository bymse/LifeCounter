using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSubscriptionsChannel : ILifeUpdatesSubscriber, ILifeUpdatesSubscriptionsProvider
{
    private static readonly Channel<LifeUpdatesSubscription> SubscriptionsChannel =
        Channel.CreateUnbounded<LifeUpdatesSubscription>();

    public async Task SubscribeAsync(Guid widgetId, string page, string connectionId)
    {
        await SubscriptionsChannel.Writer.WriteAsync(new LifeUpdatesSubscription(connectionId, widgetId, page));
    }

    public async IAsyncEnumerable<LifeUpdatesSubscription> GetSubscriptionsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        while (await SubscriptionsChannel.Reader.WaitToReadAsync(cancellationToken))
        {
            if (SubscriptionsChannel.Reader.TryRead(out var model))
            {
                yield return model;
            }
        }
    }
}