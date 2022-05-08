namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public interface ILifeUpdatesSubscriber
{
    Task SubscribeAsync(Guid widgetId, string page, string connectionId);
}