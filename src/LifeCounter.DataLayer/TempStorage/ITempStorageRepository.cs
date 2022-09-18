namespace LifeCounter.DataLayer.TempStorage;

public interface ITempStorageRepository
{
    Task SaveAsync(string key, TimeSpan ttl);
    Task<bool> ExistsAsync(string key);
}