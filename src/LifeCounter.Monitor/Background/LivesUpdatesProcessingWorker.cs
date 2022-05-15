using LifeCounter.Common.Container;
using LifeCounter.Monitor.Models.LifeUpdates;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;

namespace LifeCounter.Monitor.Background;

[PreventAutoRegistration]
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
        var requestsProvider = scope.ServiceProvider.GetService<ILifeUpdatesSubscribeRequestsProvider>()!;
        var requestHandler = scope.ServiceProvider.GetService<LifeUpdatesSubscribeRequestHandler>()!;
        
        await foreach (var request in requestsProvider.GetAsync(stoppingToken))
        {
            requestHandler.Handle(request);
        }
    }
}