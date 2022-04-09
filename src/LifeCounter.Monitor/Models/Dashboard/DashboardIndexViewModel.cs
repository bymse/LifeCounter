namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardIndexViewModel : DashboardViewModel
{
    public DashboardIndexViewModel(DashboardAppModel appModel, IReadOnlyList<DashboardRowModel> rows) : base(rows)
    {
        AppModel = appModel;
    }

    public DashboardAppModel AppModel { get; }
}