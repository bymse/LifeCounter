using LifeCounter.Site.Areas.Identity.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account;

public class Login : PageModel
{
    [BindProperty] public EmailAuthLinkRequestForm Form { get; set; } = null!;

    public void OnGet()
    {
        Form = new EmailAuthLinkRequestForm();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        throw new Exception();
    }
}