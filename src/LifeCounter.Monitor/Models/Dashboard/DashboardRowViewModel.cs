namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardRowViewModel
{
    public DashboardRowViewModel(Guid lifeId, DateTime endOfLife, IReadOnlyDictionary<string, string> properties)
    {
        LifeId = lifeId;
        EndOfLife = endOfLife;
        Properties = properties;
    }

    public Guid LifeId { get; }
    public DateTime EndOfLife { get; }
    public IReadOnlyDictionary<string, string> Properties { get; }
}