using LifeCounter.Common.Store;

namespace LifeCounter.Widget.Models;

public class AliveRequestHandler
{
    private readonly ILifeStore lifeStore;
    private readonly LifeEndProvider lifeEndProvider;

    public AliveRequestHandler(ILifeStore lifeStore, LifeEndProvider lifeEndProvider)
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
            new Dictionary<string, string>(),
            lifeEndProvider.GetLifeEnd()
        );
    }
}