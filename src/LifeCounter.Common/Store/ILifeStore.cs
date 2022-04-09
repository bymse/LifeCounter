namespace LifeCounter.Common.Store;

public interface ILifeStore
{
    Task KeepLifeAsync(Guid widgetId, string page, Guid lifeId, DateTimeOffset lifeEnd);
    Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId);
    Task FinishExpiredLivesAwait(string widget, DateTimeOffset now);
    IAsyncEnumerable<string> GetAliveWidgetsAsync();
    Task<IReadOnlyList<LifeModel>> GetAliveAsync(Guid widgetId, string page, DateTimeOffset now);
}