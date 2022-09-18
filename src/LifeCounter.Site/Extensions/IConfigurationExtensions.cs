namespace LifeCounter.Site.Extensions;

public static class IConfigurationExtensions
{
    public static TimeSpan GetTokenTtl(this IConfiguration configuration) =>
        configuration.GetValue<TimeSpan>("AuthToken:Ttl");
}