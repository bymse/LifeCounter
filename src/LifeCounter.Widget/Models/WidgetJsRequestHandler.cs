using System.Text.Json;

namespace LifeCounter.Widget.Models;

public class WidgetJsRequestHandler
{
    private readonly IWebHostEnvironment environment;
    private readonly IConfigurationProvider configurationProvider;

    public WidgetJsRequestHandler(IWebHostEnvironment environment, IConfigurationProvider configurationProvider)
    {
        this.environment = environment;
        this.configurationProvider = configurationProvider;
    }

    public string GetWidgetJs(Guid widgetId)
    {
        var path = Path.Combine(environment.ContentRootPath, configurationProvider.GetJsPath(), "widget.js");
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