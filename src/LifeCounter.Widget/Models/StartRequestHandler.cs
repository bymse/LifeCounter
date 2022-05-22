using LifeCounter.DataLayer.LifeStore;

namespace LifeCounter.Widget.Models;

public class StartRequestHandler
{
    private readonly ILifeStoreRepository lifeStore;
    private readonly LifeEndProvider lifeEndProvider;

    public StartRequestHandler(ILifeStoreRepository lifeStore, LifeEndProvider lifeEndProvider)
    {
        this.lifeStore = lifeStore;
        this.lifeEndProvider = lifeEndProvider;
    }

    public async Task<StartResponse> HandleAsync(StartRequest startRequest)
    {
        var lifeId = Guid.NewGuid();
        await lifeStore.KeepLifeAsync(
            startRequest.WidgetId,
            startRequest.Page,
            lifeId,
            startRequest.Properties,
            lifeEndProvider.GetLifeEnd()
        );
        return new StartResponse(lifeId);
    }
}