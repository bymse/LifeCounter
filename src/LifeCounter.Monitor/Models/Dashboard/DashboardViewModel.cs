namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardViewModel
{
    public DashboardViewModel(IReadOnlyList<DashboardRowModel> rows)
    {
        Rows = rows;
    }

    public IReadOnlyList<DashboardRowModel> Rows { get; }
}