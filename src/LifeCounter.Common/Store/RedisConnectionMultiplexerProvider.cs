using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace LifeCounter.Common.Store;

internal static class RedisConnectionMultiplexerProvider
{
    public static ConnectionMultiplexer GetMultiplexer(IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("Redis");
        return ConnectionMultiplexer.Connect(cs);
    }
}