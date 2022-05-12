namespace LifeCounter.Monitor.Models.LifeUpdates;

public interface ILifeUpdatesGroupsManager
{
    bool GroupsExists(string group);
    Task AddToGroupAsync(string group, string connectionId);
    IReadOnlyList<string> RemoveFromGroupsAndGetNames(string connectionId);
}