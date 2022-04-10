using LifeCounter.Common.Store;

namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardViewModelBuilder
{
    private readonly ILifeStore lifeStore;

    public DashboardViewModelBuilder(ILifeStore lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    public async Task<DashboardIndexViewModel> BuildAsync(DashboardForm form)
    {
        var (widgetId, page, _) = form;
        var alive = await lifeStore.GetAliveAsync(widgetId, page, DateTimeOffset.UtcNow);
        var rows = alive
            .Select(e => new DashboardRowModel(e.LifeId, e.LifeEnd.DateTime))
            .ToArray();

        var appModel = new DashboardAppModel(widgetId.ToString(), page);
        return new DashboardIndexViewModel(appModel, rows, form);
    }
}