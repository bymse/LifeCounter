using System.ComponentModel.DataAnnotations;

namespace LifeCounter.Widget.Validation;

public class LifeIdAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not Guid lifeId)
        {
            return false;
        }

        return lifeId != Guid.Empty;
    }
}