namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public interface ILifeUpdatesSubscriptionsProvider
{
    IAsyncEnumerable<LifeUpdatesSubscription> GetSubscriptionsAsync(CancellationToken cancellationToken);
}