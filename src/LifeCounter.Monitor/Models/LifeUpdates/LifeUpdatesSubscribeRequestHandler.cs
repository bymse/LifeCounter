using LifeCounter.Common.Container;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LifeUpdatesSubscribeRequestHandler
{
    private readonly ILifeUpdatesSubscriptionsManager subscriptionsManager;
    private readonly ILifeUpdatesUpdateSender updateSender;

    public LifeUpdatesSubscribeRequestHandler(
        ILifeUpdatesSubscriptionsManager subscriptionsManager,
        ILifeUpdatesUpdateSender updateSender
    )
    {
        this.subscriptionsManager = subscriptionsManager;
        this.updateSender = updateSender;
    }

    public void Handle(LifeUpdatesSubscribeRequest request)
    {
        subscriptionsManager.Subscribe(request, updateSender.SendAsync);
    }
}