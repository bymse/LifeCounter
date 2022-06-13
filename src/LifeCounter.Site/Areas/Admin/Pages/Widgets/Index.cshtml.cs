using System.Security.Claims;
using LifeCounter.DataLayer.Db.Repositories;
using LifeCounter.Site.Areas.Admin.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Admin.Pages.Widgets;

public class Index : PageModel
{
    private readonly ILifeCounterWidgetsRepository widgetsRepository;
    private readonly UserManager<IdentityUser> userManager;

    public Index(ILifeCounterWidgetsRepository widgetsRepository, UserManager<IdentityUser> userManager)
    {
        this.widgetsRepository = widgetsRepository;
        this.userManager = userManager;
    }

    public IReadOnlyList<WidgetCardViewModel> WidgetCards { get; set; } = null!;

    public void OnGet()
    {
        WidgetCards = widgetsRepository.GetWidgets(userManager.GetUserId(User))
            .Select(e => new WidgetCardViewModel(e.Title, e.PublicUid, e.CreatedDate))
            .ToArray();
    }
}