using LifeCounter.DataLayer.TempStorage;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCounter.DataLayer.Container;

public static class TempStorageRegisterer
{
    public static IServiceCollection UseTempStorage(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .UseRedis()
            .AddTransient<ITempStorageRepository, TempStorageRepository>();
    }
}