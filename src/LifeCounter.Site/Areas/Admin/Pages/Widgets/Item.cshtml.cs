using LifeCounter.DataLayer.Db.Entity;
using LifeCounter.DataLayer.Db.Repositories;
using LifeCounter.Site.Areas.Admin.Pages.Models;
using LifeCounter.Site.Areas.Admin.Pages.Models.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Areas.Admin.Pages.Widgets;

[ServiceFilter(typeof(WidgetAccessCheckerFilter))]
public class Item : PageModel
{
    private readonly ILifeCounterWidgetsRepository widgetsRepository;
    private readonly UserManager<IdentityUser> userManager;

    public Item(ILifeCounterWidgetsRepository widgetsRepository, UserManager<IdentityUser> userManager)
    {
        this.widgetsRepository = widgetsRepository;
        this.userManager = userManager;
    }

    [ModelBinder(typeof(WidgetEntityBinder))]
    [BindProperty(SupportsGet = true, BinderType = typeof(WidgetEntityBinder), Name = "widgetId")]
    public Widget? Widget { get; set; }

    public void OnGet()
    {
        Form = new WidgetForm
        {
            Title = Widget?.Title ?? "",
            PublicId = Widget?.PublicUid ?? Guid.NewGuid(),
            Enabled = Widget?.Enabled ?? false,
            TransportType = Widget?.TransportType ?? TransportType.SignalR
        };
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var widget = Widget ?? new Widget
        {
            OwnerId = userManager.GetUserId(User),
            CreatedDate = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Unspecified)
        };

        widget.Title = Form.Title;
        widget.PublicUid = Form.PublicId!.Value;
        widget.Enabled = Form.Enabled;
        widget.TransportType = Form.TransportType;

        if (Widget == null)
        {
            widgetsRepository.Insert(widget);
        }
        else
        {
            widgetsRepository.SaveChanges();
        }

        Widget = widget;
        return RedirectToPage(new { widgetId = widget.WidgetId });
    }

    public IActionResult OnPostDelete()
    {
        if (Widget != null)
        {
            widgetsRepository.Delete(Widget);
        }

        return RedirectToPage(nameof(Index));
    }

    [BindProperty] public WidgetForm Form { get; set; } = null!;
}