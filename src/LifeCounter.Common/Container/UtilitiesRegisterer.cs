using LifeCounter.Common.Front;
using LifeCounter.Common.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCounter.Common.Container;

public static class UtilitiesRegisterer
{
    public static IServiceCollection UseUtilities(this IServiceCollection serviceCollection, string section)
    {
        serviceCollection.AddSingleton<ICurrentAppSectionProvider>(_ => new CurrentAppSectionProvider(section));
        serviceCollection.AddScoped<IFrontBundleProvider, FrontBundleProvider>();
        return serviceCollection;
    }
}