using LifeCounter.Background;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddHostedService<LifeCleanerWorker>())
    .Build()
    .RunAsync();