namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardViewModel
{
    public DashboardViewModel(IReadOnlyList<DashboardRowViewModel> rows)
    {
        Rows = rows;
    }

    public IReadOnlyList<DashboardRowViewModel> Rows { get; }
}