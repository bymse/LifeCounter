using LifeCounter.DataLayer.Db.Repositories;

namespace LifeCounter.Widget.Models;

public class WidgetProvider
{
    private const string KEY = "LifeCounterWidget";

    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILifeCounterWidgetsRepository repository;

    public WidgetProvider(IHttpContextAccessor httpContextAccessor, ILifeCounterWidgetsRepository repository)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.repository = repository;
    }

    public DataLayer.Db.Entity.Widget? FindWidgetByPublicId(Guid id)
    {
        var key = KEY + id;
        if (httpContextAccessor.HttpContext?.Items[key] is WidgetWrapper widgetWrapper)
        {
            return widgetWrapper.Widget;
        }

        var widget = new WidgetWrapper(repository.FindWidgetByPublicId(id));
        if (httpContextAccessor.HttpContext != null)
        {
            httpContextAccessor.HttpContext.Items[key] = widget;
        }

        return widget.Widget;
    }

    private class WidgetWrapper
    {
        public readonly DataLayer.Db.Entity.Widget? Widget;

        public WidgetWrapper(DataLayer.Db.Entity.Widget? widget)
        {
            Widget = widget;
        }
    }
}