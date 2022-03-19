namespace LifeCounter.Widget.Models;

public interface IConfigurationProvider
{
    TimeSpan GetLifeLength();
}