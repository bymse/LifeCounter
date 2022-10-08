using LifeCounter.Site.Areas.Identity.Pages.Models;
using LifeCounter.Site.Models.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account;

public class Login : PageModel
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly UserTokenManager userTokenManager;
    private readonly AuthLinkEmailSender authLinkEmailSender;
    private readonly SignInManager<IdentityUser> signInManager;

    public Login(
        UserManager<IdentityUser> userManager, UserTokenManager userTokenManager,
        AuthLinkEmailSender authLinkEmailSender, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.userTokenManager = userTokenManager;
        this.authLinkEmailSender = authLinkEmailSender;
        this.signInManager = signInManager;
    }

    public EmailSendResult? SendResult { get; set; }

    [BindProperty(SupportsGet = true)] public EmailAuthLinkRequestForm? Form { get; set; }

    public IActionResult OnGet()
    {
        var returnUrl = Form?.ReturnUrl ?? "/";
        if (signInManager.IsSignedIn(User))
        {
            return Redirect(returnUrl);
        }

        Form = new EmailAuthLinkRequestForm
        {
            Email = Form?.Email,
            ReturnUrl = returnUrl
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid || Form == null)
        {
            return Page();
        }

        var user = await userManager.FindByEmailAsync(Form.Email);
        if (user != null)
        {
            var token = await userTokenManager.GenerateLoginTokenAsync(user);
            SendResult = await authLinkEmailSender.SendLoginLinkAsync(user, token, Form.ReturnUrl);
        }

        SendResult ??= EmailSendResult.Ok;

        return Page();
    }
}