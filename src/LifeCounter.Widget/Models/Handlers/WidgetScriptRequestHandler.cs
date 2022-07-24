using System.Text.Json;
using LifeCounter.Common.Front;
using LifeCounter.DataLayer.Db.Entity;
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
        return new WidgetFrontConfig
        {
            AlivePeriod = configurationProvider.GetAlivePeriod().TotalMilliseconds,
            WidgetId = widgetId,
            ApiUrl = configurationProvider.GetBaseUrl(),
            SignalrUrl = frontBundleProvider.GetBundleUrl("signalr", "js"),
            WidgetUrl = frontBundleProvider.GetBundleUrl("widget", "js"),
            TransportType = TransportType.SignalR.ToString()
        };
    }
}