using LifeCounter.DataLayer.Db.Repositories;

namespace LifeCounter.Widget.Models.Validation;

public class WidgetRequestValidator
{
    private readonly ILifeCounterWidgetsRepository repository;

    public WidgetRequestValidator(ILifeCounterWidgetsRepository repository)
    {
        this.repository = repository;
    }

    public bool IsValid(IWidgetIdHolder widgetIdHolder)
    {
        var widget = repository.FindWidgetByPublicId(widgetIdHolder.WidgetId);
        return widget?.Enabled == true;
    }
}