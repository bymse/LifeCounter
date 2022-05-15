using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSubscribeRequestChannel : ILifeUpdatesSubscriber, ILifeUpdatesSubscribeRequestProvider
{
    private static readonly Channel<LifeUpdatesSubscribeRequest> SubscriptionsChannel =
        Channel.CreateUnbounded<LifeUpdatesSubscribeRequest>();

    public async Task SubscribeAsync(Guid widgetId, string page, string connectionId)
    {
        await SubscriptionsChannel.Writer.WriteAsync(
            new LifeUpdatesSubscribeRequest(connectionId,
                new LifeUpdatesIdentifier(widgetId, page))
        );
    }

    public async IAsyncEnumerable<LifeUpdatesSubscribeRequest> GetAsync(
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