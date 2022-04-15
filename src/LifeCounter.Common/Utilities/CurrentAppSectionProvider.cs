namespace LifeCounter.Common.Utilities;

internal class CurrentAppSectionProvider : ICurrentAppSectionProvider
{
    public CurrentAppSectionProvider(string section)
    {
        Section = section;
    }

    public string Section { get; }
}