using LifeCounter.DataLayer.LifeStore;

namespace LifeCounter.Widget.Models;

public class StopRequestHandler
{
    private readonly ILifeStoreRepository lifeStore;

    public StopRequestHandler(ILifeStoreRepository lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    public Task HandleAsync(LifeRequest lifeRequest)
    {
        return lifeStore.FinishLifeAsync(lifeRequest.WidgetId, lifeRequest.Page, lifeRequest.LifeId);
    }
}