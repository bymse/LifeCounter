namespace LifeCounter.Monitor.Models;

public class DashboardRowModel
{
    public DashboardRowModel(Guid lifeId, DateTime endOfLife)
    {
        LifeId = lifeId;
        EndOfLife = endOfLife;
    }

    public Guid LifeId { get; }
    public DateTime EndOfLife { get; }
}