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

    public Task FinishExpiredLivesAwait(string widget, DateTimeOffset now)
    {
        return database.SortedSetRemoveRangeByScoreAsync(widget, double.NegativeInfinity, now.ToUnixTimeMilliseconds());
    }

    public async IAsyncEnumerable<string> GetAliveWidgetsAsync()
    {
        var endpoints = database.Multiplexer.GetEndPoints();
        foreach (var endPoint in endpoints)
        {
            var server = database.Multiplexer.GetServer(endPoint);
            await foreach (var key in server.KeysAsync(pattern: "widget:*"))
            {
                yield return key;
            }
        }
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