namespace LifeCounter.Monitor.Models.LifeUpdates;

public interface ILifeUpdatesNotificationHandler
{
    Task HandleAsync(LifeUpdatesIdentifier identifier, IReadOnlyList<string> clients);
}