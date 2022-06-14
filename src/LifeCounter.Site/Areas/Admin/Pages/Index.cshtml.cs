using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Admin.Pages;

public class Index : PageModel
{
    public IActionResult OnGet()
    {
        return RedirectToPage($"{nameof(Widgets)}/{nameof(Widgets.Index)}");
    }
}