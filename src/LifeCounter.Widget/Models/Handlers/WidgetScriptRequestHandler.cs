using System.Text.Json;
using LifeCounter.Common.Front;
using LifeCounter.Widget.Models.Dto;

namespace LifeCounter.Widget.Models.Handlers;

public class WidgetScriptRequestHandler
{
    private readonly IFrontBundleProvider frontBundleProvider;
    private readonly IConfigurationProvider configurationProvider;

    public WidgetScriptRequestHandler(
        IFrontBundleProvider frontBundleProvider,
        IConfigurationProvider configurationProvider
    )
    {
        this.frontBundleProvider = frontBundleProvider;
        this.configurationProvider = configurationProvider;
    }

    public string GetWidgetJs(WidgetScriptRequest request) => GetScript("widget", request.WidgetId);

    public string GetInvalidWidgetJs(IWidgetIdHolder widgetIdHolder) =>
        GetScript("invalid-widget", widgetIdHolder.WidgetId);

    private string GetScript(string script, Guid widgetId)
    {
        var path = frontBundleProvider.GetAbsolutePath(script, "js");
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