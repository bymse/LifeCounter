namespace LifeCounter.Widget.Models;

public class LifeEndProvider
{
    private readonly IConfigurationProvider configuration;

    public LifeEndProvider(IConfigurationProvider configuration)
    {
        this.configuration = configuration;
    }

    public DateTimeOffset GetLifeEnd()
    {
        return DateTimeOffset.UtcNow + configuration.GetLifeLength();
    }
}