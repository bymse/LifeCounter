using System.Text.Json.Serialization;
using LifeCounter.Common.Validation;

namespace LifeCounter.Widget.Models.Dto;

public class StopRequest : IWidgetIdHolder
{
    [JsonPropertyName("widgetId")]
    public Guid WidgetId { get; init; }
    
    [JsonPropertyName("page")]
    public string Page { get; init; } = null!;
    
    [RequiredGuid]
    [JsonPropertyName("lifeId")]
    public Guid LifeId { get; init; }
}