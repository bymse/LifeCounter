using System.Net;
using LifeCounter.Site.Areas.Admin.Pages.Widgets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LifeCounter.Site.Areas.Admin.Pages.Models.Infrastructure;

public class WidgetAccessCheckerFilter : IPageFilter
{
    private readonly UserManager<IdentityUser> userManager;

    public WidgetAccessCheckerFilter(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var widget = (context.HandlerInstance as Item)!.Widget;
        var currentUserId = userManager.GetUserId(context.HttpContext.User);
        if (widget != null && !widget.OwnerId.Equals(currentUserId, StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
        }
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }
}