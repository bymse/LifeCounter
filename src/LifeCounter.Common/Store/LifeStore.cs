using StackExchange.Redis;

namespace LifeCounter.Common.Store;

public class LifeStore : ILifeStore
{
    private readonly IDatabaseAsync database;

    public LifeStore(IDatabaseAsync database)
    {
        this.database = database;
    }

    public async Task KeepLifeAsync(Guid widgetId, string page, Guid lifeId, DateTimeOffset lifeEnd)
    {
        await database
            .SortedSetAddAsync(
                GetKey(widgetId, page),
                GetLifeIdStr(lifeId),
                lifeEnd.ToUnixTimeMilliseconds()
            );
    }

    public Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId)
    {
        return database.SortedSetRemoveAsync(GetKey(widgetId, page), GetLifeIdStr(lifeId));
    }

    private static string GetKey(Guid widgetId, string page)
    {
        return $"widget:{widgetId:N}:{page}";
    }

    private static string GetLifeIdStr(Guid lifeId)
    {
        return lifeId.ToString("N");
    }
}