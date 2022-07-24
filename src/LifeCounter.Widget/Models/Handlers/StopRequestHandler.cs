using LifeCounter.DataLayer.LifeStore;
using LifeCounter.Widget.Models.Dto;

namespace LifeCounter.Widget.Models.Handlers;

public class StopRequestHandler
{
    private readonly ILifeStoreRepository lifeStore;

    public StopRequestHandler(ILifeStoreRepository lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    public Task HandleAsync(StopRequest lifeRequest)
    {
        return lifeStore.FinishLifeAsync(lifeRequest.WidgetId, lifeRequest.Page, lifeRequest.LifeId);
    }
}