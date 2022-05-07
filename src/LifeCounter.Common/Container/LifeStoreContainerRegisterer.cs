using LifeCounter.Common.Store;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCounter.Common.Container;

public static class LifeStoreContainerRegisterer
{
    public static IServiceCollection UseLifeStore(this IServiceCollection serviceCollection)
    {
        return serviceCollection
                .UseRedis()
                .AddTransient<KeepLifeScriptManager>()
                .AddTransient<ILifeStore, LifeStore>()
            ;
    }
}