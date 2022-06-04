using Microsoft.Extensions.FileProviders;

namespace LifeCounter.Common.Front;

public static class FrontApplicationBuilderExtensions
{
    public static IApplicationBuilder UseFront(this WebApplication app)
    {
        var frontBundleProvider = app.Services.GetRequiredService<IFrontBundleProvider>();
        return app.UseStaticFiles(new StaticFileOptions
        {
            RequestPath = new PathString(frontBundleProvider.FrontBaseUrl),
            FileProvider = new PhysicalFileProvider(frontBundleProvider.GetBasePath())
        });
    }
}