namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardIndexViewModel : DashboardViewModel
{
    public DashboardIndexViewModel(
        DashboardAppModel appModel,
        IReadOnlyList<DashboardRowModel> rows,
        DashboardForm form
    ) : base(rows)
    {
        AppModel = appModel;
        Form = form;
    }

    public DashboardForm Form { get; }

    public DashboardAppModel AppModel { get; }
}