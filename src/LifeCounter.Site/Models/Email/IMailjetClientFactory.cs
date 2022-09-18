using Mailjet.Client;

namespace LifeCounter.Site.Models.Email;

public interface IMailjetClientFactory
{
    IMailjetClient GetClient();
}