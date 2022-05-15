using StackExchange.Redis;

namespace LifeCounter.Common.Store;

internal class GetLivesScriptManager
{
    private static readonly LuaScript Script = LuaScript.Prepare(@"
local current_time = tonumber(redis.call('TIME')[1])
redis.call('ZREMRANGEBYSCORE', @key, 0, current_time)

local lives = redis.call('ZRANGE', @key, 0, 'infinity', 'BYSCORE', 'WITHSCORES')  
local props = redis.call('HGETALL', @propsKey);

return {lives,props}
");

    private readonly IDatabaseAsync database;

    public GetLivesScriptManager(IDatabaseAsync database)
    {
        this.database = database;
    }

    public async Task<RedisResult> CallAsync(string key, string propsKey)
    {
        return await database.ScriptEvaluateAsync(Script, new { key, propsKey });
    }
}