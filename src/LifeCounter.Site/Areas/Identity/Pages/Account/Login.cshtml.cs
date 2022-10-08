using LifeCounter.DataLayer.Db.Repositories;
using LifeCounter.Site.Areas.Identity.Pages.Models;
using LifeCounter.Site.Models.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account;

public class Login : PageModel
{
    private readonly IIdentityUserRepository userRepository;
    private readonly UserAuthLinkSender authLinkSender;
    private readonly UserManager<IdentityUser> userManager;

    public Login(
        IIdentityUserRepository userRepository,
        UserAuthLinkSender userAuthLinkSender,
        UserManager<IdentityUser> userManager
    )
    {
        this.userRepository = userRepository;
        authLinkSender = userAuthLinkSender;
        this.userManager = userManager;
    }

    public EmailSendResult? SendResult { get; set; }

    [BindProperty(SupportsGet = true)] public EmailAuthLinkRequestForm? Form { get; set; }

    public void OnGet()
    {
        Form = new EmailAuthLinkRequestForm
        {
            Email = Form?.Email,
            ReturnUrl = Form?.ReturnUrl ?? "/"
        };
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await userManager.FindByEmailAsync(Form!.Email);
        if (user != null)
        {
            SendResult = await authLinkSender.SendLoginAsync(user, Form!.ReturnUrl ?? "/");
        }

        return Page();
    }
}