namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardViewModelBuilder
{
    private readonly DashboardRowsViewModelBuilder rowsViewModelBuilder;

    public DashboardViewModelBuilder(DashboardRowsViewModelBuilder rowsViewModelBuilder)
    {
        this.rowsViewModelBuilder = rowsViewModelBuilder;
    }

    public async Task<DashboardIndexViewModel> BuildAsync(DashboardForm form)
    {
        var (widgetId, page, _) = form;
        var rows = await rowsViewModelBuilder.BuildAsync(form.WidgetId, form.Page);
        var appModel = new DashboardAppModel(widgetId.ToString(), page);
        return new DashboardIndexViewModel(appModel, rows, form);
    }
}