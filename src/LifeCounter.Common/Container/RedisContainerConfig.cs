using LifeCounter.Common.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace LifeCounter.Common.Container;

internal static class RedisContainerConfig
{
    public static void RegisterRedis(IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddSingleton(p =>
            RedisConnectionMultiplexerProvider.GetMultiplexer(p.GetService<IConfiguration>()!)
        );
        serviceCollection.TryAddScoped<IDatabaseAsync>(provider
            => provider.GetService<ConnectionMultiplexer>()!.GetDatabase()
        );
    }
}