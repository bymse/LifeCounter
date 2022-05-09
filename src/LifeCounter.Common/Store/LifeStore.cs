using StackExchange.Redis;

namespace LifeCounter.Common.Store;

internal class LifeStore : ILifeStore
{
    private readonly KeepLifeScriptManager keepLifeScriptManager;
    private readonly IDatabaseAsync database;

    public LifeStore(IDatabaseAsync database, KeepLifeScriptManager keepLifeScriptManager)
    {
        this.database = database;
        this.keepLifeScriptManager = keepLifeScriptManager;
    }

    public async Task KeepLifeAsync(Guid widgetId, string page, Guid lifeId, DateTimeOffset lifeEnd)
    {
        await keepLifeScriptManager.CallAsync(
            GetKey(widgetId, page),
            GetLifeIdStr(lifeId),
            lifeEnd.ToUnixTimeSeconds()
        );
    }

    public Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId)
    {
        return database.SortedSetRemoveAsync(GetKey(widgetId, page), GetLifeIdStr(lifeId));
    }

    public async Task<IReadOnlyList<LifeModel>> GetAliveAsync(Guid widgetId, string page, DateTimeOffset now)
    {
        var values = await database.SortedSetRangeByRankWithScoresAsync(GetKey(widgetId, page));

        return values
            .Select(e => new LifeModel
            {
                LifeId = Guid.TryParse(e.Element, out var lifeId) ? lifeId : Guid.Empty,
                LifeEnd = DateTimeOffset.FromUnixTimeSeconds((long)e.Score),
            })
            .ToArray();
    }

    public ChannelMessageQueue Subscribe(Guid widgetId, string page)
    {
        return database.Multiplexer
            .GetSubscriber()
            .Subscribe($"channel:{GetKey(widgetId, page)}");
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