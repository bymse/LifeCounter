using System.Text.Json.Serialization;

namespace LifeCounter.Widget.Models;

public class StartRequest
{
    [JsonPropertyName("widgetId")]
    public Guid WidgetId { get; init; }
    
    [JsonPropertyName("page")]
    public string Page { get; init; } = null!;
}