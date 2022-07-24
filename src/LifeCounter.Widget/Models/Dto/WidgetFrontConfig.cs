using System.Text.Json.Serialization;

namespace LifeCounter.Widget.Models.Dto;

public class WidgetFrontConfig
{
    [JsonPropertyName("alivePeriod")]
    public double AlivePeriod { get; init; }
    
    [JsonPropertyName("widgetId")]
    public Guid WidgetId { get; init; }
    
    [JsonPropertyName("apiUrl")]
    public string ApiUrl { get; init; }
    
    [JsonPropertyName("widgetUrl")]
    public string WidgetUrl { get; init; }
    
    [JsonPropertyName("signalrUrl")]
    public string SignalrUrl { get; init; }
    
    [JsonPropertyName("transportType")]
    public string? TransportType { get; init; }
}