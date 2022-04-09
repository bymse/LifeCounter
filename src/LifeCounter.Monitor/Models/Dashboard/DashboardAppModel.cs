using System.Text.Json.Serialization;

namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardAppModel
{
    public DashboardAppModel(string widgetId, string page)
    {
        WidgetId = widgetId;
        Page = page;
    }

    [JsonPropertyName("widgetId")]
    public string WidgetId { get; }
    
    [JsonPropertyName("page")]
    public string Page { get; }
}