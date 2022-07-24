namespace LifeCounter.Site.Areas.Admin.Pages.Models;

public record WidgetCardViewModel(string Title, Guid WidgetId, Guid PublicId, DateTime Created, bool Enabled);