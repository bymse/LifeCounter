namespace LifeCounter.Widget.Models;

public class ConfigurationProvider : IConfigurationProvider
{
    private readonly IConfiguration configuration;

    public ConfigurationProvider(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public TimeSpan GetLifeLength()
    {
        return configuration.GetValue<TimeSpan>("Life:DefaultLifeLength");
    }
}