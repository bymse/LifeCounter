using Microsoft.AspNetCore.Identity;

namespace LifeCounter.DataLayer.Db.Repositories;

public class IdentityUserRepository : IIdentityUserRepository
{
    private readonly LifeCounterDbContext dbContext;

    public IdentityUserRepository(LifeCounterDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IdentityUser? FinderUser(string? email)
    {
        return dbContext.Users.FirstOrDefault(e => e.Email == email);
    }
}