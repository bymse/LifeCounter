using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LifeCounter.Site.TagHelpers;

[HtmlTargetElement("form-card")]
public class FormCardTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "section";
        output.AddClass("form-card", HtmlEncoder.Default);
        var content = (await output.GetChildContentAsync()).GetContent();
        output.Content.SetHtmlContent($@"
    <div class=""form-card__form-wrapper"">
        {content}
    </div>
");
    }
}