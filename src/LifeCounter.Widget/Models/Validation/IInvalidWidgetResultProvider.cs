using LifeCounter.Common.Container;
using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Models.Validation;

[PreventAutoRegistration]
public interface IInvalidWidgetResultProvider
{
    IActionResult GetResult(IWidgetIdHolder widgetIdHolder);
}