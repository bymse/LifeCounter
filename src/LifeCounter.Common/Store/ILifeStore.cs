namespace LifeCounter.Common.Store;

public interface ILifeStore
{
    Task KeepLifeAsync(Guid widgetId, string page, Guid lifeId, TimeSpan lifeLength);
    Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId);
}