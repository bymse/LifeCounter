using LifeCounter.Site.Areas.Identity.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account;

public class VerifyLogin : PageModel
{
    private readonly UserLoginTokenProcessor loginTokenProcessor;

    public VerifyLogin(UserLoginTokenProcessor loginTokenProcessor)
    {
        this.loginTokenProcessor = loginTokenProcessor;
    }

    [BindProperty(SupportsGet = true)] public AuthTokenVerifyForm Form { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await loginTokenProcessor.ProcessAsync(Form);
        return Redirect(Form.ReturnUrl);
    }
}