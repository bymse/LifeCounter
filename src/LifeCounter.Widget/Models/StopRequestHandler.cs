using LifeCounter.Common.Store;

namespace LifeCounter.Widget.Models;

public class StopRequestHandler
{
    private readonly ILifeStore lifeStore;

    public StopRequestHandler(ILifeStore lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    public Task HandleAsync(LifeRequest lifeRequest)
    {
        return lifeStore.FinishLifeAsync(lifeRequest.WidgetId, lifeRequest.Page, lifeRequest.LifeId);
    }
}