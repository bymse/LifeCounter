using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Models.Validation;

public class InvalidWidgetApiResultProvider : IInvalidWidgetResultProvider
{
    public IActionResult GetResult(IWidgetIdHolder widgetIdHolder) => new BadRequestResult();
}