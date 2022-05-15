namespace LifeCounter.Monitor.Models.LifeUpdates;

public record LifeUpdatesIdentifier(Guid WidgetId, string Page)
{
    public string GetKey() => $"{WidgetId:N}:{Page}";
}