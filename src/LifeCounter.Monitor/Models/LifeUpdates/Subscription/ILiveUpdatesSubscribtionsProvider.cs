namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public interface ILifeUpdatesSubscribeRequestsProvider
{
    IAsyncEnumerable<LifeUpdatesSubscribeRequest> GetAsync(CancellationToken cancellationToken);
}