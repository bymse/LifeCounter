using LifeCounter.Common.Container;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;

namespace LifeCounter.Monitor.Models.LifeUpdates;

public class LifeUpdatesSubscribeRequestHandler
{
    private readonly ILifeUpdatesSubscriptionsManager subscriptionsManager;
    private readonly ILifeUpdatesNotificationHandler notificationHandler;

    public LifeUpdatesSubscribeRequestHandler(
        ILifeUpdatesSubscriptionsManager subscriptionsManager,
        ILifeUpdatesNotificationHandler notificationHandler
    )
    {
        this.subscriptionsManager = subscriptionsManager;
        this.notificationHandler = notificationHandler;
    }

    public void Handle(LifeUpdatesSubscribeRequest request)
    {
        subscriptionsManager.Subscribe(request, notificationHandler.HandleAsync);
    }
}