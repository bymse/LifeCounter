using LifeCounter.Monitor.Models.LifeUpdates.Subscription;

namespace LifeCounter.Monitor.Background;

public class LivesUpdatesProcessingWorker : BackgroundService
{
    private readonly IServiceProvider serviceProvider;

    public LivesUpdatesProcessingWorker(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var sessionsProvider = scope.ServiceProvider.GetService<ILifeUpdatesSessionsProvider>()!;
        var sessionsHandler = scope.ServiceProvider.GetService<LifeUpdatesSubscriptionsManager>()!;
        await foreach (var session in sessionsProvider.GetAsync(stoppingToken))
        {
            await sessionsHandler.SubscribeAsync(session);
        }
    }
}