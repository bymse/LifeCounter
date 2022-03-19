using LifeCounter.Common.Store;

namespace LifeCounter.Widget.Models;

public class StartRequestHandler
{
    private readonly ILifeStore lifeStore;
    private readonly IConfigurationProvider configuration;

    public StartRequestHandler(ILifeStore lifeStore, IConfigurationProvider configuration)
    {
        this.lifeStore = lifeStore;
        this.configuration = configuration;
    }

    public async Task<StartResponse> HandleAsync(StartRequest startRequest)
    {
        var lifeId = Guid.NewGuid();
        await lifeStore.KeepLifeAsync(startRequest.WidgetId, startRequest.Page, lifeId, configuration.GetLifeLength());
        return new StartResponse(lifeId);
    }
}