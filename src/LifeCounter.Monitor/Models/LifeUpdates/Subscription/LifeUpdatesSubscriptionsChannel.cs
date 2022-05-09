using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSessionsChannel : ILifeUpdatesSubscriber, ILifeUpdatesSessionsProvider
{
    private static readonly Channel<LifeUpdatesSession> SubscriptionsChannel =
        Channel.CreateUnbounded<LifeUpdatesSession>();

    public async Task SubscribeAsync(Guid widgetId, string page, string connectionId)
    {
        await SubscriptionsChannel.Writer.WriteAsync(new LifeUpdatesSession(connectionId, widgetId, page));
    }

    public async IAsyncEnumerable<LifeUpdatesSession> GetAsync(
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