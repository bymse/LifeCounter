namespace LifeCounter.Widget.Models.Validation;

public class WidgetRequestValidator
{
    private readonly WidgetProvider widgetProvider;

    public WidgetRequestValidator(WidgetProvider widgetProvider)
    {
        this.widgetProvider = widgetProvider;
    }

    public bool IsValid(IWidgetIdHolder widgetIdHolder)
    {
        var widget = widgetProvider.FindWidgetByPublicId(widgetIdHolder.WidgetId);
        return widget?.Enabled == true;
    }
}