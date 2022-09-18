using StackExchange.Redis;

namespace LifeCounter.DataLayer.TempStorage;

public class TempStorageRepository : ITempStorageRepository
{
    private readonly IDatabaseAsync database;

    public TempStorageRepository(IDatabaseAsync database)
    {
        this.database = database;
    }

    public async Task SaveAsync(string key, TimeSpan ttl)
    {
        await database.StringSetAsync(GetKey(key), true, ttl);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return (await database.StringGetAsync(GetKey(key))).HasValue;
    }

    private static string GetKey(string key) => $"TempStorage:{key}";
}