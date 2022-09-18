using LifeCounter.DataLayer.Db.Repositories;
using LifeCounter.Site.Areas.Identity.Pages.Models;
using LifeCounter.Site.Models.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account;

public class Login : PageModel
{
    private readonly IIdentityUserRepository userRepository;
    private readonly UserAuthLinkSender authLinkSender;

    public Login(IIdentityUserRepository userRepository, UserAuthLinkSender userAuthLinkSender)
    {
        this.userRepository = userRepository;
        authLinkSender = userAuthLinkSender;
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

        var user = userRepository.FinderUser(Form!.Email);
        if (user != null)
        {
            SendResult = await authLinkSender.SendLoginAsync(user, Form!.ReturnUrl ?? "/");
        }

        return Page();
    }
}