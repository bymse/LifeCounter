using LifeCounter.Common.Front;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LifeCounter.Common.Extensions;

public static class RazorPageExtensions
{
    public static TagBuilder Css(this IRazorPage page, string name)
    {
        var frontBundleProvider = page.ViewContext.HttpContext.RequestServices.GetRequiredService<IFrontBundleProvider>();
        var script = new TagBuilder("link");
        script.Attributes.Add(new("rel", "stylesheet"));
        script.Attributes.Add(new("type", "text/css"));
        script.Attributes.Add(new("href", frontBundleProvider.GetBundleUrl(name, "css")));
        return script;
    }
    
    public static TagBuilder Js(this IRazorPage page, string name)
    {
        var frontBundleProvider = page.ViewContext.HttpContext.RequestServices.GetRequiredService<IFrontBundleProvider>();
        var script = new TagBuilder("script");
        script.Attributes.Add(new("src", frontBundleProvider.GetBundleUrl(name, "js")));
        script.Attributes.Add(new("defer", "true"));
        return script;
    }
}