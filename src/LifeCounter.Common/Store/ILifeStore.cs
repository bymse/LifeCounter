using StackExchange.Redis;

namespace LifeCounter.Common.Store;

public interface ILifeStore
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