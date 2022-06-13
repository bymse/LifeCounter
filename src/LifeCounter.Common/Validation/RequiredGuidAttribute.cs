using System.ComponentModel.DataAnnotations;

namespace LifeCounter.Common.Validation;

public class RequiredGuidAttribute : ValidationAttribute
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