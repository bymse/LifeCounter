namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public interface ILifeUpdatesSubscribeRequestProvider
{
    IAsyncEnumerable<LifeUpdatesSubscribeRequest> GetAsync(CancellationToken cancellationToken);
}