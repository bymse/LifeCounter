namespace LifeCounter.Widget.Models;

public interface IConfigurationProvider
{
    TimeSpan GetLifeLength();
    TimeSpan GetAlivePeriod();
    string GetJsPath();
    string GetApiUrl();
}