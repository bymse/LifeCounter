namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public record LifeUpdatesSession(string ClientId, Guid WidgetId, string Page)
{
    public string GetKey() => $"{WidgetId:N}:{Page}";
};
