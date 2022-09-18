using LifeCounter.Site.Models.Email;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class EmailAuthLinkRequestFormViewModel
{
    public string Title { get; init; }
    public Func<object, object> HintText { get; init; }
    public EmailAuthLinkRequestForm Form { get; init; }
    public EmailSendResult? SendResult { get; init; }
}