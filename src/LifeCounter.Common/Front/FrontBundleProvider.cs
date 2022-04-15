using LifeCounter.Common.Utilities;
using Microsoft.Extensions.Hosting;

namespace LifeCounter.Common.Front;

internal class FrontBundleProvider : IFrontBundleProvider
{
    private readonly IHostEnvironment hostEnvironment;
    private readonly ICurrentAppSectionProvider currentAppSectionProvider;

    public FrontBundleProvider(
        IHostEnvironment hostEnvironment,
        ICurrentAppSectionProvider currentAppSectionProvider
    )
    {
        this.hostEnvironment = hostEnvironment;
        this.currentAppSectionProvider = currentAppSectionProvider;
    }

    public string FrontBaseUrl => $"/{currentAppSectionProvider.Section}/static";

    public string GetAbsolutePath(string bundle, string extension)
        => GetBundle(bundle, extension).FullName;

    public string GetBasePath() 
        => GetBundlesDir().FullName;

    public string GetBundleUrl(string bundle, string extension)
        => $"{FrontBaseUrl}/{GetBundle(bundle, extension).Name}";

    private FileInfo GetBundle(string bundle, string extension)
    {
        return GetBundlesDir()
            .EnumerateFiles($"{bundle}.*.{extension}")
            .First();
    }

    private DirectoryInfo GetBundlesDir()
    {
        var root = hostEnvironment.ContentRootPath;
        var clientAppPath = Path.Combine(
            root, "..",
            "LifeCounter.ClientApp", "build", currentAppSectionProvider.Section
        );

        return new DirectoryInfo(clientAppPath);
    }
}