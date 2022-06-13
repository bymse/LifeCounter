using System.ComponentModel.DataAnnotations;
using LifeCounter.Common.Validation;

namespace LifeCounter.Site.Areas.Admin.Pages.Models;

public class WidgetForm
{
    [RequiredGuid]
    [Display(Name = "Public ID")]
    public Guid? PublicId { get; init; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(512)]
    [Display(Name = "Title")]
    public string Title { get; init; } = "";
}