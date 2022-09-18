using Mailjet.Client;

namespace LifeCounter.Site.Models.Email;

public class MailjetClientFactory : IMailjetClientFactory
{
    private readonly IConfiguration configuration;

    public MailjetClientFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IMailjetClient GetClient()
    {
        return new MailjetClient(
            configuration.GetValue<string>("Mailjet:ApiKey"),
            configuration.GetValue<string>("Mailjet:SecretKey")
        );
    }
}