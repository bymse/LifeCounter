namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public delegate Task OnMessage(LifeUpdatesIdentifier identifier, IReadOnlyList<string> clients);

public interface ILifeUpdatesSubscriptionsManager
{
    void Subscribe(LifeUpdatesSubscribeRequest request, OnMessage onMessage);

    void Unsubscribe(string connectionId);
}