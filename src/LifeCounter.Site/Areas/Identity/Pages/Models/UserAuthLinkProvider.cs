using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class UserAuthLinkProvider
{
    private readonly IConfiguration configuration;
    private readonly IUrlHelper urlHelper;

    public UserAuthLinkProvider(IConfiguration configuration, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
    {
        this.configuration = configuration;
        urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);
    }

    public string GetLoginLink(IdentityUser user, string token, string returnUrl)
        => GetUrl("VerifyLogin", user.Email, token, returnUrl);

    private string GetUrl(string page, string email, string token, string returnUrl)
    {
        var host = configuration.GetValue<string>("PublicHost");
        return urlHelper.Page(
            $"/Account/{page}",
            null,
            new { area = "Identity", email, token, returnUrl },
            Uri.UriSchemeHttps,
            host
        )!;
    }
}