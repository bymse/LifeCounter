using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LifeCounter.DataLayer.Db;

public class LifeCounterDbContextDesignTimeFactory : IDesignTimeDbContextFactory<LifeCounterDbContext>
{
    public LifeCounterDbContext CreateDbContext(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new []
                {
                    new KeyValuePair<string, string>("ConnectionStrings:PgSql", "Host=localhost;Port=5432;Database=LifeCounter;Username=postgres")
                })
            ;
        
        return new LifeCounterDbContext(configBuilder.Build());
    }
}