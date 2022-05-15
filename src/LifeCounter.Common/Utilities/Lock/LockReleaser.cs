using LifeCounter.Common.Container;

namespace LifeCounter.Common.Utilities.Lock;

[PreventAutoRegistration]
internal class LockReleaser: IDisposable
{
    private readonly Dictionary<string, Counter<SemaphoreSlim>> semaphoreSlims;
    private readonly string key;
    
    public LockReleaser(string key, Dictionary<string, Counter<SemaphoreSlim>> semaphoreSlims)
    {
        this.key = key;
        this.semaphoreSlims = semaphoreSlims;
    }
    
    public void Dispose()
    {
        Counter<SemaphoreSlim> item;
        lock (semaphoreSlims)
        {
            item = semaphoreSlims[key];
            --item.Count;
            if (item.Count == 0)
                semaphoreSlims.Remove(key);
        }
        item.Value.Release();
    }
}