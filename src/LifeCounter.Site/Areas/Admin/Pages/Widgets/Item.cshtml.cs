using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Admin.Pages.Widgets;

public class Item : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid? WidgetId { get; set; }
    
    public void OnGet()
    {
        
    }
}