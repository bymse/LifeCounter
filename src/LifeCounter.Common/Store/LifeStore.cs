using StackExchange.Redis;

namespace LifeCounter.Common.Store;

internal class LifeStore : ILifeStore
{
    private readonly KeepLifeScriptManager keepLifeScriptManager;
    private readonly GetLivesScriptManager getLivesScriptManager;
    private readonly IDatabaseAsync database;

    public LifeStore(
        IDatabaseAsync database,
        KeepLifeScriptManager keepLifeScriptManager,
        GetLivesScriptManager getLivesScriptManager
    )
    {
        this.database = database;
        this.keepLifeScriptManager = keepLifeScriptManager;
        this.getLivesScriptManager = getLivesScriptManager;
    }

    public async Task KeepLifeAsync(
        Guid widgetId,
        string page,
        Guid lifeId,
        IReadOnlyDictionary<string, string> properties,
        DateTimeOffset lifeEnd
    )
    {
        var lifeKey = GetLifeIdStr(lifeId);
        var widgetKey = GetKey(widgetId, page);

        await keepLifeScriptManager.CallAsync(
            widgetKey,
            lifeKey,
            lifeEnd.ToUnixTimeSeconds(),
            GetPropsKey(widgetKey),
            PropertiesHelper.ToStoredString(properties)
        );
    }

    public Task FinishLifeAsync(Guid widgetId, string page, Guid lifeId)
    {
        return database.SortedSetRemoveAsync(GetKey(widgetId, page), GetLifeIdStr(lifeId));
    }

    public async Task<IReadOnlyList<LifeModel>> GetAliveAsync(Guid widgetId, string page)
    {
        var key = GetKey(widgetId, page);
        var results = (RedisResult[])await getLivesScriptManager.CallAsync(key, GetPropsKey(key));

        return LifeModelHelper.FromRedisResult(results);
    }

    public ChannelMessageQueue Subscribe(Guid widgetId, string page)
    {
        return database.Multiplexer
            .GetSubscriber()
            .Subscribe($"__keyspace@*__:{GetKey(widgetId, page)}");
    }

    private static string GetKey(Guid widgetId, string page) => $"widget:{widgetId:N}:page:{page}";

    private static string GetLifeIdStr(Guid lifeId) => lifeId.ToString("N");

    private static string GetPropsKey(string key) => $"props:{key}";
}