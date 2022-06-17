using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeCounter.Site.Pages;

public class Demo : PageModel
{
    private readonly IConfiguration configuration;

    public Demo(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Guid DemoWidgetId => configuration.GetValue<Guid>("Demo:WidgetId");
    
    public void OnGet()
    {
        
    }
}