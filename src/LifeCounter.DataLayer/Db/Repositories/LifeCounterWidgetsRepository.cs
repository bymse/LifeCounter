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
            .AsNoTracking()
            .Where(e => e.OwnerId == ownerId)
            .ToArray();
    }

    public Widget? FindWidget(Guid id)
    {
        return dbContext
            .Widgets
            .Include(e => e.Owner)
            .FirstOrDefault(e => e.WidgetId == id);
    }

    public void SaveChanges() => dbContext.SaveChanges();
    
    public void Insert(Widget widget)
    {
        dbContext.Widgets.Add(widget);
        SaveChanges();
    }

    public void Delete(Widget widget)
    {
        dbContext.Widgets.Remove(widget);
        SaveChanges();
    }
}