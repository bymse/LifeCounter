using LifeCounter.DataLayer.Db.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Admin.Pages.Widgets;

public class List : PageModel
{
    private readonly ILifeCounterWidgetsRepository widgetsRepository;

    public List(ILifeCounterWidgetsRepository widgetsRepository)
    {
        this.widgetsRepository = widgetsRepository;
    }

    public void OnGet()
    { 
        
    }
}