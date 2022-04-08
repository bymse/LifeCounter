using LifeCounter.Background;
using LifeCounter.Common.Container;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        LifeStoreContainerConfig.RegisterLifeStore(services);
        services.AddHostedService<LifeCleanerWorker>();
    })
    .Build()
    .RunAsync();