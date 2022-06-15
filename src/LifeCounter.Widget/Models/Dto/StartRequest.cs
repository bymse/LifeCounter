using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace LifeCounter.Widget.Models.Dto;

public class StartRequest : IWidgetIdHolder
{
    [JsonPropertyName("widgetId")]
    public Guid WidgetId { get; init; }
    
    [JsonPropertyName("page")]
    public string Page { get; init; } = null!;
    
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, string>? Properties { get; init; } = ImmutableDictionary<string, string>.Empty;
}