using System.Net.Http.Headers;
using System.Text;
using LifeCounter.Common.Utilities;
using Mailjet.Client;

namespace LifeCounter.Site.Models.Email;

public class MailgunEmailSender : IEmailSender
{
    private readonly HttpClient httpClient;

    private readonly IViewRenderService viewRenderService;
    private readonly IConfiguration configuration;

    public MailgunEmailSender(
        HttpClient httpClient,
        IViewRenderService viewRenderService,
        IConfiguration configuration
    )
    {
        this.httpClient = httpClient;
        this.viewRenderService = viewRenderService;
        this.configuration = configuration;
    }

    public async Task<EmailSendResult> SendAsync(string email, string subject, string bodyView, object model)
    {
        httpClient.BaseAddress = new Uri(configuration.GetValue<string>("Mailgun:Url"));
     
        var authString = $"api:{(string?)configuration.GetValue<string>("Mailgun:ApiKey")}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(Encoding.Default.GetBytes(authString)));

        var request = new Dictionary<string, string>()
        {
            { "from", configuration.GetValue<string>("Email:SendFrom") },
            { "to", email },
            { "subject", subject },
            { "html", await viewRenderService.RenderToStringAsync($"Mail/{bodyView}", model)}
        };
        return await SendAsync(httpClient, request);
    }

    private static async Task<EmailSendResult> SendAsync(HttpClient client, IDictionary<string, string> request)
    {
        try
        {
            var content = new FormUrlEncodedContent(request);
            var responseMessage = await client.PostAsync("messages", content);
            responseMessage.EnsureSuccessStatusCode();
            return EmailSendResult.Ok;
        }
        catch
        {
            return EmailSendResult.Error;
        }
    }
}