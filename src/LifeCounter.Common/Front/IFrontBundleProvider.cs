namespace LifeCounter.Common.Front;

public interface IFrontBundleProvider
{
    string FrontBaseUrl { get; }
    string GetAbsolutePath(string bundle, string extension);
    string GetBundleUrl(string bundle, string extension);
}