using System.Text.Json;

namespace LifeCounter.Widget.Models;

public class WidgetJsRequestHandler
{
    private readonly IWebHostEnvironment environment;
    private readonly IConfigurationProvider configurationProvider;

    private readonly Lazy<string> widgetLazy;

    public WidgetJsRequestHandler(IWebHostEnvironment environment, IConfigurationProvider configurationProvider)
    {
        this.environment = environment;
        this.configurationProvider = configurationProvider;
        widgetLazy = new Lazy<string>(LoadWidget);
    }

    public string GetWidgetJs() => widgetLazy.Value;

    private string LoadWidget()
    {
        var path = Path.Combine(environment.ContentRootPath, configurationProvider.GetJsPath(), "widget.js");
        var js = File.ReadAllText(path);
        var configStr = JsonSerializer.Serialize(GetConfig());
        return js.Replace("@CONFIG_INJECTION@", configStr);
    }

    private object GetConfig()
    {
        return new { alivePeriod = configurationProvider.GetAlivePeriod().TotalMilliseconds };
    }
}