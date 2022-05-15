using StackExchange.Redis;

namespace LifeCounter.Common.Store;

internal class KeepLifeScriptManager
{
    private static readonly LuaScript Script = LuaScript.Prepare(@"
local current_time = tonumber(redis.call('TIME')[1])
redis.call('ZREMRANGEBYSCORE', @key, 0, current_time)

redis.call('ZADD', @key, @score, @value)
redis.call('HSET', @propsKey, @value, @properties) 

redis.call('EXPIREAT', @key, @score)
redis.call('EXPIREAT', @propsKey, @score)
");

    private readonly IDatabaseAsync database;

    public KeepLifeScriptManager(IDatabaseAsync database)
    {
        this.database = database;
    }

    public async Task CallAsync(string key, string value, long score, string propsKey, string properties)
    {
        await database.ScriptEvaluateAsync(Script, new
        {
            key, value, score,
            propsKey,
            properties
        });
    }
}