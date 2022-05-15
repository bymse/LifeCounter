using LifeCounter.Common.Store;

namespace LifeCounter.Widget.Models;

public class StartRequestHandler
{
    private readonly ILifeStore lifeStore;
    private readonly LifeEndProvider lifeEndProvider;

    public StartRequestHandler(ILifeStore lifeStore, LifeEndProvider lifeEndProvider)
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