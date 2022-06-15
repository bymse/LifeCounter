using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Models.Validation;

public interface IInvalidWidgetResultProvider
{
    IActionResult GetResult(IWidgetIdHolder widgetIdHolder);
}