using LifeCounter.Common.Store;

namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardRowsViewModelBuilder
{
    private readonly ILifeStore lifeStore;

    public DashboardRowsViewModelBuilder(ILifeStore lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    public async Task<IReadOnlyList<DashboardRowViewModel>> BuildAsync(Guid widgetId, string page)
    {
        var alive = await lifeStore.GetAliveAsync(widgetId, page);
        return alive
            .Select(e => new DashboardRowViewModel(e.LifeId, e.LifeEnd.DateTime, e.Properties))
            .ToArray();
    }
}