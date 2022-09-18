namespace LifeCounter.Site.Models.Email;

public interface IEmailSender
{
    Task<EmailSendResult> SendAsync(string email, string subject, string bodyView, object model);
}