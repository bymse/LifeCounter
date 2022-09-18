using Microsoft.AspNetCore.Identity;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class AuthTokenManager
{
    private const string LOGIN_PURPOSE = "auth-link-login";

    private readonly UserManager<IdentityUser> userManager;

    public AuthTokenManager(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<string> GetLoginTokenAsync(IdentityUser user)
    {
        return await userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, LOGIN_PURPOSE);
    }

    public async Task<bool> IsValidLoginTokenAsync(IdentityUser user, string token)
    {
        return await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, LOGIN_PURPOSE, token);
    }
}