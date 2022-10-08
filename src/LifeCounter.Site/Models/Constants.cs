using LifeCounter.Site.Models.TokenProvider;
using Microsoft.AspNetCore.Identity;

namespace LifeCounter.Site.Models;

public static class Constants
{
    public const string AUTH_TOKEN_PROVIDER = nameof(SingleValidateTokenProvider<IdentityUser>);
    public const string EMAIL_AUTH_METHOD = "AuthLinkEmail";
}