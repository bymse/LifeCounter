namespace LifeCounter.Common.Utilities.Lock;

public sealed class NamedLock
{
    private readonly Dictionary<string, Counter<SemaphoreSlim>> semaphoreSlims = new();

    private SemaphoreSlim GetOrCreate(string key)
    {
        Counter<SemaphoreSlim>? item;
        lock (semaphoreSlims)
        {
            if (semaphoreSlims.TryGetValue(key, out item))
            {
                item.Count++;
            }
            else
            {
                item = new Counter<SemaphoreSlim>(new SemaphoreSlim(1, 1));
                semaphoreSlims[key] = item;
            }
        }
        return item.Value;
    }

    public IDisposable Lock(string key)
    {
        GetOrCreate(key).Wait();
        return new LockReleaser(key, semaphoreSlims);
    }

    public async Task<IDisposable> LockAsync(string key)
    {
        await GetOrCreate(key).WaitAsync().ConfigureAwait(false);
        return new LockReleaser(key, semaphoreSlims);
    }
}