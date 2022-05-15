using System.Net;

namespace LifeCounter.Common.Store;

internal static class PropertiesHelper
{
    public static string ToStoredString(IReadOnlyDictionary<string, string> properties)
    {
        var pairs = properties
            .Where(e => !string.IsNullOrEmpty(e.Key))
            .Select(e => $"{WebUtility.UrlEncode(e.Key)}={WebUtility.UrlEncode(e.Value)}");
        return string.Join("&", pairs);
    }

    public static IReadOnlyDictionary<string, string> FromStoredString(string stored)
    {
        return stored.Split("&")
            .Select(e =>
            {
                var split = e.Split('=');
                return new
                {
                    key = WebUtility.UrlDecode(split.First()),
                    value = WebUtility.UrlDecode(split.Last())
                };
            })
            .Where(e => !string.IsNullOrEmpty(e.key))
            .ToDictionary(e => e.key, e => e.value);
    }
}