using System.Security.Cryptography;
using System.Text;
using LifeCounter.DataLayer.TempStorage;
using LifeCounter.Site.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LifeCounter.Site.Models.TokenProvider;

public class SingleValidateTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    private readonly ITempStorageRepository tempStorageRepository;
    private readonly IConfiguration configuration;
    
    public SingleValidateTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<DataProtectionTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger, ITempStorageRepository tempStorageRepository, IConfiguration configuration) : base(dataProtectionProvider, options, logger)
    {
        this.tempStorageRepository = tempStorageRepository;
        this.configuration = configuration;
    }

    public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
        var hash = SHA1.HashData(Encoding.Default.GetBytes(token));
        var key = $"{purpose}:{await manager.GetUserIdAsync(user)}:{Convert.ToBase64String(hash)}";
        if (await tempStorageRepository.ExistsAsync(key))
        {
            return false;
        }

        var isValid = await base.ValidateAsync(purpose, token, manager, user);
        if (isValid)
        {
            await tempStorageRepository.SaveAsync(key, configuration.GetTokenTtl());
        }

        return isValid;
    }
}