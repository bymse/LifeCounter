using LifeCounter.Common.Utilities;
using Mailjet.Client.TransactionalEmails;
using Newtonsoft.Json;

namespace LifeCounter.Site.Models.Email;

public class MailjetEmailSender : IEmailSender
{
    private readonly IMailjetClientFactory mailjetClientFactory;
    private readonly IViewRenderService viewRenderService;
    private readonly IConfiguration configuration;

    public MailjetEmailSender(IMailjetClientFactory mailjetClientFactory, IViewRenderService viewRenderService, IConfiguration configuration)
    {
        this.mailjetClientFactory = mailjetClientFactory;
        this.viewRenderService = viewRenderService;
        this.configuration = configuration;
    }

    public async Task<EmailSendResult> SendAsync(string email, string subject, string bodyView, object model)
    {
        var request = new TransactionalEmail
        {
            Subject = subject,
            HTMLPart = await viewRenderService.RenderToStringAsync($"Mail/{bodyView}", model),
            To = new List<SendContact>
            {
                new(email)
            },
            From = new SendContact(configuration.GetValue<string>("Email:SendFrom"))
        };
        var response = await mailjetClientFactory.GetClient().SendTransactionalEmailAsync(request);
        var message = response.Messages?.FirstOrDefault();
        return message is not { Errors: null } ? EmailSendResult.Error : EmailSendResult.Ok;
    }
}