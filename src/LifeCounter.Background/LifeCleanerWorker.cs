using LifeCounter.Common.Container;
using LifeCounter.Common.Store;

namespace LifeCounter.Background;

[PreventAutoRegistration]
public class LifeCleanerWorker : BackgroundService
{
    private readonly ILifeStore lifeStore;

    public LifeCleanerWorker(ILifeStore lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await foreach (var aliveWidget in lifeStore.GetAliveWidgetsAsync().WithCancellation(stoppingToken))
            {
                await lifeStore.FinishExpiredLivesAwait(aliveWidget, DateTimeOffset.UtcNow);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}