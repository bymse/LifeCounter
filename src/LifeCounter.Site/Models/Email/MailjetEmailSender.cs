using Mailjet.Client.TransactionalEmails;
using Newtonsoft.Json;

namespace LifeCounter.Site.Models.Email;

public class MailjetEmailSender : IEmailSender
{
    private readonly IMailjetClientFactory mailjetClientFactory;

    public MailjetEmailSender(IMailjetClientFactory mailjetClientFactory)
    {
        this.mailjetClientFactory = mailjetClientFactory;
    }

    public async Task<EmailSendResult> SendAsync(string email, string subject, string bodyViewPath, object model)
    {
        var request = new TransactionalEmail
        {
            Subject = subject,
            To = new List<SendContact>
            {
                new(email)
            }
        };
        var response = await mailjetClientFactory.GetClient().SendTransactionalEmailAsync(request);
        return response.Messages.First().Errors.Any() ? EmailSendResult.Error : EmailSendResult.Ok;
    }
}