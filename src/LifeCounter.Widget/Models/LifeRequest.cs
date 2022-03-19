using System.Text.Json.Serialization;
using LifeCounter.Widget.Validation;

namespace LifeCounter.Widget.Models;

public class LifeRequest
{
    [JsonPropertyName("widgetId")]
    public Guid WidgetId { get; init; }
    
    [JsonPropertyName("page")]
    public string Page { get; init; } = null!;
    
    [LifeId]
    [JsonPropertyName("lifeId")]
    public Guid LifeId { get; init; }
}