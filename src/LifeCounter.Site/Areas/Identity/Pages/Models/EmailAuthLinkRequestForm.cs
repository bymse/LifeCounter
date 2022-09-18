using System.ComponentModel.DataAnnotations;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class EmailAuthLinkRequestForm
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }
    
    public string? ReturnUrl { get; set; }
}