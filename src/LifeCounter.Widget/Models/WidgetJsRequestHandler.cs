using System.Text.Json;
using LifeCounter.Common.Front;

namespace LifeCounter.Widget.Models;

public class WidgetJsRequestHandler
{
    private readonly IFrontBundleProvider frontBundleProvider;
    private readonly IConfigurationProvider configurationProvider;

    public WidgetJsRequestHandler(
        IFrontBundleProvider frontBundleProvider,
        IConfigurationProvider configurationProvider
    )
    {
        this.frontBundleProvider = frontBundleProvider;
        this.configurationProvider = configurationProvider;
    }

    public string GetWidgetJs(Guid widgetId)
    {
        var path = frontBundleProvider.GetAbsolutePath("widget", "js");
        var js = File.ReadAllText(path);
        var configStr = JsonSerializer.Serialize(GetConfig(widgetId));
        return js.Replace("@CONFIG_INJECTION@", configStr.Replace("\"", "\\\""));
    }

    private object GetConfig(Guid widgetId)
    {
        return new
        {
            alivePeriod = configurationProvider.GetAlivePeriod().TotalMilliseconds,
            widgetId,
            apiUrl = configurationProvider.GetApiUrl()
        };
    }
}