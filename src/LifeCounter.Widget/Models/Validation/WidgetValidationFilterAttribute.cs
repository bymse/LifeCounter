using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Models.Validation;

public class WidgetValidationFilterAttribute : TypeFilterAttribute
{
    public WidgetValidationFilterAttribute(Type invalidResultProviderType) : base(
        typeof(WidgetValidationFilter<>).MakeGenericType(invalidResultProviderType)
    )
    {
    }
}