using LifeCounter.DataLayer.Db.Entity;

namespace LifeCounter.DataLayer.Db.Repositories;

public interface ILifeCounterWidgetsRepository
{
    IReadOnlyList<Widget> GetWidgets(string ownerId);
    Widget? FindWidgetByInternalId(Guid id);
    void SaveChanges();
    void Insert(Widget widget);
    void Delete(Widget widget);
    Widget? FindWidgetByPublicId(Guid id);
}