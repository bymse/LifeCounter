namespace LifeCounter.Monitor.Models;

public class DashboardIndexViewModel : DashboardViewModel
{
    public DashboardIndexViewModel(DashboardAppModel appModel, IReadOnlyList<DashboardRowModel> rows) : base(rows)
    {
        AppModel = appModel;
    }

    public DashboardAppModel AppModel { get; }
}