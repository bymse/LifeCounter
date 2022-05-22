using StackExchange.Redis;

namespace LifeCounter.DataLayer.LifeStore;

public interface ILifeStoreRepository
{
    Task KeepLifeAsync(Guid widgetId,
        string page,
        Guid lifeId,
        IReadOnlyDictionary<string, string> properties,
        DateTimeOffset lifeEnd
    );

    Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId);
    Task<IReadOnlyList<LifeModel>> GetAliveAsync(Guid widgetId, string page);
    ChannelMessageQueue Subscribe(Guid widgetId, string page);
}