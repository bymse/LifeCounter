namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardRowViewModel
{
    public DashboardRowViewModel(Guid lifeId, DateTime endOfLife)
    {
        LifeId = lifeId;
        EndOfLife = endOfLife;
    }

    public Guid LifeId { get; }
    public DateTime EndOfLife { get; }
}