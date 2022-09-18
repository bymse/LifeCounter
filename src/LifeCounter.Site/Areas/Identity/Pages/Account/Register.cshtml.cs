using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using LifeCounter.Site.Areas.Identity.Pages.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace LifeCounter.Site.Areas.Identity.Pages.Account
{
    public class Register : PageModel
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
}
