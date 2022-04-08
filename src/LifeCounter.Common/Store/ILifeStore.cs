namespace LifeCounter.Common.Store;

public interface ILifeStore
{
    Task KeepLifeAsync(Guid widgetId, string page, Guid lifeId, DateTimeOffset lifeEnd);
    Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId);
}