using LifeCounter.Site.Models;
using LifeCounter.Site.Models.Email;
using Microsoft.AspNetCore.Identity;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class AuthLinkEmailSender
{
    private readonly UserAuthLinkProvider authLinkProvider;
    private readonly IEmailSender emailSender;

    public AuthLinkEmailSender(UserAuthLinkProvider authLinkProvider, IEmailSender emailSender)
    {
        this.authLinkProvider = authLinkProvider;
        this.emailSender = emailSender;
    }

    public async Task<EmailSendResult> SendRegistrationAsync(IdentityUser user, string token)
    {
        var viewModel = new AuthLinkEmailViewModel
        {
            Url = authLinkProvider.GetRegistrationLink(user, token),
        };
        return await emailSender
            .SendAsync(user.Email, "LifeCounter. Confirm registration", "RegistrationLink", viewModel);
    }

    public async Task<EmailSendResult> SendLoginLinkAsync(IdentityUser user, string token, string? returnUrl)
    {
        var viewModel = new AuthLinkEmailViewModel
        {
            Url = authLinkProvider.GetLoginLink(user, token, returnUrl),
        };
        return await emailSender
            .SendAsync(user.Email, "LifeCounter. Log in link", "LoginLink", viewModel);
    }
}