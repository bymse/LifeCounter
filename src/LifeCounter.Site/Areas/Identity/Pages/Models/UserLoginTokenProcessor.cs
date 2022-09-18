using LifeCounter.DataLayer.Db.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class UserLoginTokenProcessor
{
    private readonly AuthTokenManager authTokenManager;
    private readonly IIdentityUserRepository identityUserRepository;
    private readonly SignInManager<IdentityUser> signInManager;

    public UserLoginTokenProcessor(AuthTokenManager authTokenManager, IIdentityUserRepository identityUserRepository,
        SignInManager<IdentityUser> signInManager)
    {
        this.authTokenManager = authTokenManager;
        this.identityUserRepository = identityUserRepository;
        this.signInManager = signInManager;
    }

    public async Task ProcessAsync(AuthTokenVerifyForm form)
    {
        var user = identityUserRepository.FinderUser(form.Email);
        if (user != null && await authTokenManager.IsValidLoginTokenAsync(user, form.Token))
        {
            await signInManager.SignInAsync(user, true);
        }
    }
}