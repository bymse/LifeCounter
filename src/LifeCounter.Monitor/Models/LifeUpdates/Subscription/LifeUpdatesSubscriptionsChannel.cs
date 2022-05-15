using System.Runtime.CompilerServices;
using System.Threading.Channels;
using LifeCounter.Common.Container;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

[PreventAutoRegistration]
public class LifeUpdatesSubscribeRequestsChannel : ILifeUpdatesSubscriber, ILifeUpdatesSubscribeRequestsProvider
{
    private static readonly Channel<LifeUpdatesSubscribeRequest> SubscriptionsChannel =
        Channel.CreateUnbounded<LifeUpdatesSubscribeRequest>();

    public async Task SubscribeAsync(Guid widgetId, string page, string connectionId)
    {
        var identifier = new LifeUpdatesIdentifier(widgetId, page);
        await SubscriptionsChannel.Writer.WriteAsync(new LifeUpdatesSubscribeRequest(connectionId, identifier));
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