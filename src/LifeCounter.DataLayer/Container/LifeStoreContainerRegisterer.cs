using LifeCounter.DataLayer.LifeStore;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCounter.DataLayer.Container;

public static class LifeStoreContainerRegisterer
{
    public static IServiceCollection UseLifeStore(this IServiceCollection serviceCollection)
    {
        return serviceCollection
                .UseRedis()
                .AddTransient<KeepLifeScriptManager>()
                .AddTransient<GetLivesScriptManager>()
                .AddTransient<FinishLifeScriptManager>()
                .AddTransient<ILifeStoreRepository, LifeStoreRepository>()
            ;
    }
}