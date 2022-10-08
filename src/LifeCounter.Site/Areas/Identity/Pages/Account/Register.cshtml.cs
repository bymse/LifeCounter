using LifeCounter.Site.Areas.Identity.Pages.Models;
using LifeCounter.Site.Models.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Identity.Pages.Account
{
    public class Register : PageModel
    {
        private readonly AuthLinkEmailSender authLinkEmailSender;
        private readonly UserManager<IdentityUser> userManager;

        public Register(UserManager<IdentityUser> userManager, AuthLinkEmailSender authLinkEmailSender)
        {
            this.userManager = userManager;
            this.authLinkEmailSender = authLinkEmailSender;
        }

        [BindProperty] public EmailAuthLinkRequestForm Form { get; set; } = null!;
        public EmailSendResult? SendResult { get; set; }

        public void OnGet()
        {
            Form = new EmailAuthLinkRequestForm();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new IdentityUser();
            await userManager.SetEmailAsync(user, Form.Email);
            await userManager.SetUserNameAsync(user, Form.Email);
            var result = await userManager.CreateAsync(user);

            const string duplicateEmail = "DuplicateEmail";
            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                SendResult = await authLinkEmailSender.SendRegistrationAsync(user, token);
            }
            else if (result.Errors.All(e => e.Code == duplicateEmail))
            {
                SendResult = EmailSendResult.Ok;
            }
            else
            {
                foreach (var error in result.Errors.Where(e => e.Code != duplicateEmail))
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
