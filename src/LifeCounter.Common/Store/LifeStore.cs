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

    public async Task<IReadOnlyList<LifeModel>> GetAliveAsync(Guid widgetId, string page, DateTimeOffset now)
    {
        var values = await database.SortedSetRangeByRankWithScoresAsync(
            GetKey(widgetId, page),
            now.ToUnixTimeMilliseconds()
        );

        return values
            .Select(e => new LifeModel
            {
                LifeId = Guid.TryParse(e.Element, out var lifeId) ? lifeId : Guid.Empty,
                LifeEnd = DateTimeOffset.FromUnixTimeMilliseconds((long)e.Score),
            })
            .ToArray();
    }

    private static string GetKey(Guid widgetId, string page)
    {
        return $"widget:{widgetId:N}:page:{page}";
    }

    private static string GetLifeIdStr(Guid lifeId)
    {
        return lifeId.ToString("N");
    }
}