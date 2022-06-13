using LifeCounter.DataLayer.Db.Entity;
using LifeCounter.Site.Areas.Admin.Pages.Models.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Admin.Pages.Widgets;

[ServiceFilter(typeof(WidgetAccessCheckerFilter))]
public class Item : PageModel
{
    [BindProperty(SupportsGet = true, BinderType = typeof(WidgetEntityBinder), Name = "widgetId")]
    public Widget? Widget { get; set; }
    
    public void OnGet()
    {
        
    }
}