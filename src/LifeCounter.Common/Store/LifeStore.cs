using StackExchange.Redis;

namespace LifeCounter.Common.Store;

public class LifeStore : ILifeStore
{
    private readonly IDatabaseAsync database;

    public LifeStore(IDatabaseAsync database)
    {
        this.database = database;
    }

    public async Task KeepLifeAsync(Guid widgetId, string page, Guid lifeId, TimeSpan lifeLength)
    {
        await database.SetAddAsync(GetKey(widgetId, page), GetLifeIdStr(lifeId));
    }

    public Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId)
    {
        return database.SetRemoveAsync(
            GetKey(widgetId, page),
            GetLifeIdStr(lifeId)
        );
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