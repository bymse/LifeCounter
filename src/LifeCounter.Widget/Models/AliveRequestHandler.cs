using LifeCounter.Common.Store;

namespace LifeCounter.Widget.Models;

public class AliveRequestHandler
{
    private readonly ILifeStore lifeStore;
    private readonly IConfigurationProvider configurationProvider;

    public AliveRequestHandler(ILifeStore lifeStore, IConfigurationProvider configurationProvider)
    {
        this.lifeStore = lifeStore;
        this.configurationProvider = configurationProvider;
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
            configurationProvider.GetLifeLength()
        );
    }
}