using LifeCounter.Site.Models;
using LifeCounter.Site.Models.Email;
using Microsoft.AspNetCore.Identity;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class UserAuthLinkSender
{
    private readonly IEmailSender emailSender;
    private readonly UserAuthLinkProvider authLinkProvider;
    private readonly AuthTokenManager authTokenManager;

    public UserAuthLinkSender(IEmailSender emailSender, UserAuthLinkProvider authLinkProvider, AuthTokenManager authTokenManager)
    {
        this.emailSender = emailSender;
        this.authLinkProvider = authLinkProvider;
        this.authTokenManager = authTokenManager;
    }

    public async Task<EmailSendResult> SendLoginAsync(IdentityUser user, string returnUrl)
    {
        var token = await authTokenManager.GetLoginTokenAsync(user);
        var viewModel = new AuthLinkEmailViewModel
        {
            Url = authLinkProvider.GetLoginLink(user, token, returnUrl)
        };
        
        return await emailSender.SendAsync(user.Email, "Log in", "AuthLink", viewModel);
    }
}