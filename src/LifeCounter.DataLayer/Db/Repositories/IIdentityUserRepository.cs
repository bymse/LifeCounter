using Microsoft.AspNetCore.Identity;

namespace LifeCounter.DataLayer.Db.Repositories;

public interface IIdentityUserRepository
{
    IdentityUser? FinderUser(string? email);
}