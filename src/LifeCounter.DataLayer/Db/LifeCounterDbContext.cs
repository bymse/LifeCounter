using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LifeCounter.DataLayer.Db;

public class LifeCounterDbContext : IdentityDbContext
{
    private readonly IConfiguration configuration;

    public LifeCounterDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseNpgsql(configuration.GetConnectionString("PgSql"));
        }
        
        base.OnConfiguring(optionsBuilder);
    }
}