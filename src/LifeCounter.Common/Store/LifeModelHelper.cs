using System.Collections.Immutable;
using StackExchange.Redis;

namespace LifeCounter.Common.Store;

internal static class LifeModelHelper
{
    public static IReadOnlyList<LifeModel> FromRedisResult(RedisResult[] results)
    {
        var livesToProps = ((RedisResult[])results[1])
            .Chunk(2)
            .Select(e => new
            {
                lifeId = Guid.TryParse((string)e.First(), out var lifeId) ? (Guid?)lifeId : null,
                props = (string)e.Last()
            })
            .Where(e => e.lifeId.HasValue)
            .ToDictionary(e => e.lifeId!.Value, e => e.props);


        return ((RedisResult[])results[0])
            .Chunk(2)
            .Select(e =>
            {
                var lifeIdGuid = Guid.TryParse((string)e.First(), out var lifeId) ? lifeId : Guid.Empty;
                return new LifeModel
                {
                    LifeId = lifeIdGuid,
                    LifeEnd = DateTimeOffset.FromUnixTimeSeconds(
                        long.TryParse((string)e.Last(), out var unixSeconds)
                            ? unixSeconds
                            : 0
                    ),
                    
                    Properties = livesToProps.TryGetValue(lifeIdGuid, out var props)
                        ? PropertiesHelper.FromStoredString(props)
                        : ImmutableDictionary<string, string>.Empty
                };
            }).ToArray();
    }
}