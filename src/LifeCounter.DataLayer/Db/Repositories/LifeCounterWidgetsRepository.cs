using LifeCounter.DataLayer.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace LifeCounter.DataLayer.Db.Repositories;

internal class LifeCounterWidgetsRepository : ILifeCounterWidgetsRepository
{
    private readonly LifeCounterDbContext dbContext;

    public LifeCounterWidgetsRepository(LifeCounterDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IReadOnlyList<Widget> GetWidgets(string ownerId)
    {
        return dbContext
            .Widgets
            .Include(e => e.Owner)
            .AsNoTracking()
            .Where(e => e.OwnerId == ownerId)
            .ToArray();
    }
}