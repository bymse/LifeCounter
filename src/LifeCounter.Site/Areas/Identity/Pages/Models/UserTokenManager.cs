using LifeCounter.Site.Models;
using Microsoft.AspNetCore.Identity;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class UserTokenManager
{
    private const string LOGIN_PURPOSE = "auth-link-login";

    private readonly UserManager<IdentityUser> userManager;

    public UserTokenManager(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<string> GenerateLoginTokenAsync(IdentityUser user)
    {
        return await userManager.GenerateUserTokenAsync(user, Constants.AUTH_TOKEN_PROVIDER, LOGIN_PURPOSE);
    }

    public async Task<bool> IsValidLoginTokenAsync(IdentityUser user, string token)
    {
        return await userManager.VerifyUserTokenAsync(user, Constants.AUTH_TOKEN_PROVIDER, LOGIN_PURPOSE, token);
    }
}