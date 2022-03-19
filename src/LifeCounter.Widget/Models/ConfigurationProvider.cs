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

    public TimeSpan GetAlivePeriod()
    {
        return configuration.GetValue<TimeSpan>("Life:AlivePeriod");
    }

    public string GetJsPath()
    {
        return configuration.GetValue<string>("JsPath");
    }

    public string GetApiUrl()
    {
        return configuration.GetValue<string>("ApiUrl");
    }
}