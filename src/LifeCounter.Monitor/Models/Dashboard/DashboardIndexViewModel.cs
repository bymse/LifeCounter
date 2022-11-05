namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardIndexViewModel
{
    public DashboardIndexViewModel(
        DashboardAppModel appModel,
        DashboardForm form,
        AliveTableViewModel aliveTableViewModel
    ) 
    {
        AppModel = appModel;
        Form = form;
        AliveTableViewModel = aliveTableViewModel;
    }

    public DashboardAppModel AppModel { get; }
    public DashboardForm Form { get; }
    public AliveTableViewModel AliveTableViewModel { get; }
}