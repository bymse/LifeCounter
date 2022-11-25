using LifeCounter.Common.Container;

namespace LifeCounter.Site.Models.Email;

[PreventAutoRegistration]
public interface IEmailSender
{
    Task<EmailSendResult> SendAsync(string email, string subject, string bodyView, object model);
}