using LifeCounter.Site.Areas.Identity.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account;

public class VerifyAuth : PageModel
{
    [BindProperty(SupportsGet = true)] public AuthTokenVerifyForm Form { get; set; }
    
    public async Task<IActionResult> OnGetLogin()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await loginTokenProcessor.ProcessAsync(Form);
        return Redirect(Form.ReturnUrl);
    }

    public async Task<IActionResult> OnGetRegisterConfirm()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
    }
}