using LifeCounter.Background;
using LifeCounter.Common.Container;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .UseLifeStore()
            .AddHostedService<LifeCleanerWorker>();
    })
    .Build()
    .RunAsync();