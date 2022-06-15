using LifeCounter.DataLayer.Db;
using LifeCounter.DataLayer.Db.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCounter.DataLayer.Container;

public static class DbContainerRegisterer
{
    public static IServiceCollection AddDb(this IServiceCollection serviceCollection)
    {
        return serviceCollection
                .AddDbContext<LifeCounterDbContext>()
                .AddTransient<ILifeCounterWidgetsRepository, LifeCounterWidgetsRepository>()
            ;
    }
}