using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class UserAuthLinkProvider
{
    private readonly IConfiguration configuration;
    private readonly IUrlHelper urlHelper;

    public UserAuthLinkProvider(
        IConfiguration configuration,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor
    )
    {
        this.configuration = configuration;
        urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);
    }

    public string GetLoginLink(IdentityUser user, string token, string? returnUrl)
        => GetUrl("login", user.Email, token, returnUrl);

    public string GetRegistrationLink(IdentityUser user, string token)
        => GetUrl("RegisterConfirm", user.Email, token, null);

    private string GetUrl(string handler, string email, string token, string? returnUrl)
    {
        var host = configuration.GetValue<string>("PublicHost");
        var form = new AuthTokenVerifyForm
        {
            Email = email,
            Token = token,
            ReturnUrl = returnUrl
        };
        return urlHelper.Page(
            "/Account/VerifyAuth",
            handler,
            form,
            Uri.UriSchemeHttps,
            host
        )!;
    }
}