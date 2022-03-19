using System.Text.Json.Serialization;

namespace LifeCounter.Widget.Models;

public class StartResponse
{
    public StartResponse(Guid lifeId)
    {
        LifeId = lifeId;
    }

    [JsonPropertyName("lifeId")]
    public Guid LifeId { get; }
}