using System.Collections.Immutable;
using System.Text.Json.Serialization;
using LifeCounter.Common.Validation;

namespace LifeCounter.Widget.Models;

public class LifeRequest : IWidgetIdHolder
{
    [JsonPropertyName("widgetId")]
    public Guid WidgetId { get; init; }
    
    [JsonPropertyName("page")]
    public string Page { get; init; } = null!;
    
    [RequiredGuid]
    [JsonPropertyName("lifeId")]
    public Guid LifeId { get; init; }
    
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, string>? Properties { get; init; } = ImmutableDictionary<string, string>.Empty;
}