using System.ComponentModel.DataAnnotations;

namespace LifeCounter.Site.Areas.Identity.Pages.Models;

public class AuthTokenVerifyForm
{
    [Required]
    public string Email { get; init; }
    
    public string Token { get; init; }
    
    public string ReturnUrl { get; init; }
}