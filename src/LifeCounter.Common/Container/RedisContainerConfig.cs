using LifeCounter.Common.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LifeCounter.Common.Container;

public static class RedisContainerConfig
{
    public static void RegisterRedis(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(p =>
            RedisConnectionMultiplexerProvider.GetMultiplexer(p.GetService<IConfiguration>()!)
        );
        serviceCollection.AddScoped<IDatabaseAsync>(provider
            => provider.GetService<ConnectionMultiplexer>()!.GetDatabase()
        );
    }
}