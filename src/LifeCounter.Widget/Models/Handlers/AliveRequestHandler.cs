using LifeCounter.DataLayer.LifeStore;
using LifeCounter.Widget.Models.Dto;

namespace LifeCounter.Widget.Models.Handlers;

public class AliveRequestHandler
{
    private readonly ILifeStoreRepository lifeStore;
    private readonly LifeEndProvider lifeEndProvider;

    public AliveRequestHandler(ILifeStoreRepository lifeStore, LifeEndProvider lifeEndProvider)
    {
        this.lifeStore = lifeStore;
        this.lifeEndProvider = lifeEndProvider;
    }

    public Task HandleAsync(LifeRequest lifeRequest)
    {
        if (lifeRequest.LifeId == Guid.Empty)
        {
            return Task.CompletedTask;
        }

        return lifeStore.KeepLifeAsync(
            lifeRequest.WidgetId,
            lifeRequest.Page,
            lifeRequest.LifeId,
            lifeRequest.Properties,
            lifeEndProvider.GetLifeEnd()
        );
    }
}