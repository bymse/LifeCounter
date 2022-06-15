using LifeCounter.Common.Validation;

namespace LifeCounter.Widget.Models;

public interface IWidgetIdHolder
{
    [RequiredGuid]
    Guid WidgetId { get; }
}