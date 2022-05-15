using System.Net;

namespace LifeCounter.Common.Store;

internal static class PropertiesHelper
{
    public static string ToStoredString(IReadOnlyDictionary<string, string> properties)
    {
        var pairs = properties.Select(e => $"{WebUtility.UrlEncode(e.Key)}={WebUtility.UrlEncode(e.Value)}");
        return string.Join("&", pairs);
    }

    public static IReadOnlyDictionary<string, string> FromStoredString(string stored)
    {
        return stored.Split("&")
            .Chunk(2)
            .Select(e => new
            {
                key = WebUtility.UrlDecode(e.First()),
                value = WebUtility.UrlDecode(e.Last())
            })
            .ToDictionary(e => e.key, e => e.value);
    }
}