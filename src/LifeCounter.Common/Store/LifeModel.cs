namespace LifeCounter.Common.Store;

public class LifeModel
{
    public Guid LifeId { get; init; }
    public DateTimeOffset LifeEnd { get; init; }
}