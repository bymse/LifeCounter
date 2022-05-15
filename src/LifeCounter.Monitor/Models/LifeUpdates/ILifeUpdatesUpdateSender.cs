namespace LifeCounter.Monitor.Models.LifeUpdates;

public interface ILifeUpdatesUpdateSender
{
    Task SendAsync(LifeUpdatesIdentifier identifier, IReadOnlyList<string> clients);
}