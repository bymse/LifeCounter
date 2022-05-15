using System.Collections.Immutable;

namespace LifeCounter.Common.Store;

public class LifeModel
{
    public Guid LifeId { get; init; }
    public DateTimeOffset LifeEnd { get; init; }
    public IReadOnlyDictionary<string, string> Properties { get; init; } = ImmutableDictionary<string, string>.Empty;
}