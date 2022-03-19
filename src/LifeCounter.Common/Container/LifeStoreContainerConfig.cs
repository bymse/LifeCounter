using LifeCounter.Common.Store;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCounter.Common.Container;

public static class LifeStoreContainerConfig
{
    public static void RegisterLifeStore(IServiceCollection serviceCollection)
    {
        RedisContainerConfig.RegisterRedis(serviceCollection);
        serviceCollection.AddScoped<ILifeStore, LifeStore>();
    }
}