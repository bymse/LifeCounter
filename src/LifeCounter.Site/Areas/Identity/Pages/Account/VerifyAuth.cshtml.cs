using LifeCounter.Site.Areas.Identity.Pages.Models;
using LifeCounter.Site.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account;

public class VerifyAuth : PageModel
{
    private readonly UserTokenManager userTokenManager;
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public VerifyAuth(UserTokenManager userTokenManager, UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        this.userTokenManager = userTokenManager;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [BindProperty(SupportsGet = true)] public AuthTokenVerifyForm Form { get; set; } = null!;

    public async Task<IActionResult> OnGetLogin()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await userManager.FindByEmailAsync(Form.Email);
        if (user != null && await userTokenManager.IsValidLoginTokenAsync(user, Form.Token))
        {
            await SignIn(user);
            return RedirectToReturnUrl();
        }

        ModelState.AddModelError(string.Empty, "Log in link is not valid");
        return Page();
    }

    public async Task<IActionResult> OnGetRegisterConfirm()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await userManager.FindByEmailAsync(Form.Email);
        if (user != null)
        {
            var result = await userManager.ConfirmEmailAsync(user, Form.Token);
            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToReturnUrl();
            }
        }
        
        ModelState.AddModelError(string.Empty, "Error confirming your email.");
        return Page();
    }
    
    private async Task SignIn(IdentityUser user) =>
        await signInManager.SignInAsync(user, true, Constants.EMAIL_AUTH_METHOD);

    private IActionResult RedirectToReturnUrl() => Redirect(Form.ReturnUrl ?? "/");
}