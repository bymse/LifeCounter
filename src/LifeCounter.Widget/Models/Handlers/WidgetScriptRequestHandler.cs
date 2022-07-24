using System.Text.Json;
using LifeCounter.Common.Front;
using LifeCounter.DataLayer.Db.Entity;
using LifeCounter.Widget.Models.Dto;

namespace LifeCounter.Widget.Models.Handlers;

public class WidgetScriptRequestHandler
{
    private readonly IFrontBundleProvider frontBundleProvider;
    private readonly IConfigurationProvider configurationProvider;
    private readonly WidgetProvider widgetProvider;

    public WidgetScriptRequestHandler(
        IFrontBundleProvider frontBundleProvider,
        IConfigurationProvider configurationProvider, WidgetProvider widgetProvider)
    {
        this.frontBundleProvider = frontBundleProvider;
        this.configurationProvider = configurationProvider;
        this.widgetProvider = widgetProvider;
    }

    public string GetWidgetJs(WidgetScriptRequest request) => GetScript("loader", request.WidgetId);

    public string GetInvalidWidgetJs(IWidgetIdHolder widgetIdHolder) =>
        GetScript("invalid-loader", widgetIdHolder.WidgetId);

    private string GetScript(string script, Guid widgetId)
    {
        var path = frontBundleProvider.GetAbsolutePath(script, "js");
        var js = File.ReadAllText(path);
        var configStr = JsonSerializer.Serialize(GetConfig(widgetId));
        return js.Replace("@CONFIG_INJECTION@", configStr.Replace("\"", "\\\""));
    }

    private WidgetFrontConfig GetConfig(Guid widgetId)
    {
        var widget = widgetProvider.FindWidgetByPublicId(widgetId);
        return new WidgetFrontConfig
        {
            AlivePeriod = configurationProvider.GetAlivePeriod().TotalMilliseconds,
            WidgetId = widgetId,
            ApiUrl = configurationProvider.GetBaseUrl(),
            SignalrUrl = frontBundleProvider.GetBundleUrl("signalr", "js"),
            WidgetUrl = frontBundleProvider.GetBundleUrl("widget", "js"),
            TransportType = widget?.TransportType.ToString()
        };
    }
}