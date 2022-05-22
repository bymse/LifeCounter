using StackExchange.Redis;

namespace LifeCounter.DataLayer.LifeStore;

internal class FinishLifeScriptManager
{
    private static readonly LuaScript Script = LuaScript.Prepare(@"
redis.call('ZREM', @key, @value)
redis.call('HDEL', @propsKey, @value)
");

    private readonly IDatabaseAsync database;

    public FinishLifeScriptManager(IDatabaseAsync database)
    {
        this.database = database;
    }

    public Task CallAsync(string key, string value, string propsKey)
    {
        return database.ScriptEvaluateAsync(Script, new { key, value, propsKey });
    }
}