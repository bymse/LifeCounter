using LifeCounter.DataLayer.Db.Entity;

namespace LifeCounter.DataLayer.Db.Repositories;

public interface ILifeCounterWidgetsRepository
{
    IReadOnlyList<Widget> GetWidgets(string ownerId);
    Widget? FindWidget(Guid id);
}