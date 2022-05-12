using System.Collections.Concurrent;
using System.Reflection;
using LifeCounter.Monitor.Hubs;
using Microsoft.AspNetCore.SignalR;
using HubGroupList =
    System.Collections.Generic.IReadOnlyCollection<System.Collections.Concurrent.ConcurrentDictionary<string,
        Microsoft.AspNetCore.SignalR.HubConnectionContext>>;
using GroupConnectionList =
    System.Collections.Concurrent.ConcurrentDictionary<string, Microsoft.AspNetCore.SignalR.HubConnectionContext>;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LifeUpdatesGroupManager : ILifeUpdatesGroupsManager
{
    private readonly HubGroupList groups;
    private readonly ConcurrentDictionary<string, GroupConnectionList> innerGroups;
    private readonly IHubContext<LifeUpdatesHub> hubContext;

    public LifeUpdatesGroupManager(LifeUpdatesHubLifetimeManager<LifeUpdatesHub> lifeUpdatesHubLifetimeManager,
        IHubContext<LifeUpdatesHub> hubContext)
    {
        this.hubContext = hubContext;
        groups = GetGroups(lifeUpdatesHubLifetimeManager);
        innerGroups = GetInnerGroups(groups);
    }

    public bool GroupsExists(string group) => innerGroups.ContainsKey(group);

    public Task AddToGroupAsync(string group, string connectionId) =>
        hubContext.Groups.AddToGroupAsync(connectionId, @group);

    public IReadOnlyList<string> RemoveFromGroupsAndGetNames(string connectionId)
    {
        var result = new List<string>();
        var groupNames = innerGroups.Where(x => x.Value.ContainsKey(connectionId)).Select(e => e.Key);
        foreach (var groupName in groupNames)
        {
            Remove(groups, connectionId, groupName);
            result.Add(groupName);
        }

        return result;
    }

    private static void Remove(HubGroupList groupList, string connectionId, string group)
    {
        var method = groupList.GetType()
            .GetMethod("Remove", BindingFlags.Instance | BindingFlags.Public,
                new[] { typeof(string), typeof(string) }
            )!;

        method.Invoke(groupList, new object[] { connectionId, group });
    }

    private static HubGroupList GetGroups(LifeUpdatesHubLifetimeManager<LifeUpdatesHub> manager)
    {
        var fieldInfo = typeof(LifeUpdatesHubLifetimeManager<LifeUpdatesHub>)
            .BaseType!
            .GetField("_groups", BindingFlags.Instance | BindingFlags.NonPublic)!;

        return (HubGroupList)fieldInfo.GetValue(manager)!;
    }

    private static ConcurrentDictionary<string, GroupConnectionList> GetInnerGroups(HubGroupList groups)
    {
        var fieldInfo = groups.GetType()
            .GetField("_groups", BindingFlags.NonPublic | BindingFlags.Instance)!;
        return (ConcurrentDictionary<string, GroupConnectionList>)fieldInfo.GetValue(groups)!;
    }
}