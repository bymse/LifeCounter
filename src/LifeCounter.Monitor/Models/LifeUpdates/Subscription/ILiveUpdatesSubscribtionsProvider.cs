namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public interface ILifeUpdatesSessionsProvider
{
    IAsyncEnumerable<LifeUpdatesSession> GetAsync(CancellationToken cancellationToken);
}