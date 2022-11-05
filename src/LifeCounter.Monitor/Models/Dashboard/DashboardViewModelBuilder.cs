namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardViewModelBuilder
{
    private readonly AliveTableViewModelBuilder aliveTableViewModelBuilder;

    public DashboardViewModelBuilder(AliveTableViewModelBuilder aliveTableViewModelBuilder)
    {
        this.aliveTableViewModelBuilder = aliveTableViewModelBuilder;
    }

    public async Task<DashboardIndexViewModel> BuildAsync(DashboardForm form)
    {
        var (widgetId, page, _) = form;
        var tableViewModel = await aliveTableViewModelBuilder.BuildAsync(form.WidgetId, form.Page);
        var appModel = new DashboardAppModel(widgetId.ToString(), page);
        return new DashboardIndexViewModel(appModel, form, tableViewModel);
    }
}