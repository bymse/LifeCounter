namespace LifeCounter.Monitor.Models.Dashboard;

public class AliveTableViewModel
{
    public AliveTableViewModel(
        AliveTableColumn<Guid> lifeIds,
        AliveTableColumn<DateTime> lifeEnds,
        AliveTableColumn<IReadOnlyDictionary<string, string>> properties
        )
    {
        LifeIds = lifeIds;
        LifeEnds = lifeEnds;
        Properties = properties;
    }

    public AliveTableColumn<Guid> LifeIds { get; }
    public AliveTableColumn<DateTime> LifeEnds { get; }
    public AliveTableColumn<IReadOnlyDictionary<string, string>> Properties { get; }
}