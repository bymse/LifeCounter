using Newtonsoft.Json;

namespace LifeCounter.Site.Models.Email;

public class EmailSender : IEmailSender
{
    public async Task<EmailSendResult> SendAsync(string email, string subject, string bodyViewPath, object model)
    {
        Console.WriteLine(JsonConvert.SerializeObject(model));
        return EmailSendResult.Ok;
    }
}