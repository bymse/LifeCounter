namespace LifeCounter.Background;

public class LifeCleanerWorker : BackgroundService
{
    private readonly ILogger<LifeCleanerWorker> _logger;

    public LifeCleanerWorker(ILogger<LifeCleanerWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}