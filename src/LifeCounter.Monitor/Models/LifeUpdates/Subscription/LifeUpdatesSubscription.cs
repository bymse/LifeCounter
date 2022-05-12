namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSubscription
{
    public LifeUpdatesSubscription(Guid widgetId, string page)
    {
        WidgetId = widgetId;
        Page = page;
    }

    public Guid WidgetId { get; }
    public string Page { get; }
}