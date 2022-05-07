using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Monitor.Models.Dashboard;

public class DashboardForm
{
    [Required]
    public Guid WidgetId { get; set; }

    [Required] public string Page { get; set; } = "/";

    [HiddenInput] public bool ShowForm { get; set; } = true;

    public void Deconstruct(out Guid widgetId, out string page, out bool showForm)
    {
        widgetId = WidgetId;
        showForm = ShowForm;
        page = Page;
    }
}