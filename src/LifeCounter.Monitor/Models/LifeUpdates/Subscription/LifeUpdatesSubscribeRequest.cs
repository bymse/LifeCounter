namespace LifeCounter.Monitor.Models.LifeUpdates.Subscription;

public class LifeUpdatesSubscribeRequest
{
    public LifeUpdatesSubscribeRequest(string connectionId, LifeUpdatesIdentifier identifier)
    {
        ConnectionId = connectionId;
        Identifier = identifier;
    }

    public string ConnectionId { get; }
    public LifeUpdatesIdentifier Identifier { get; }

    public void Deconstruct(out string connectionId, out LifeUpdatesIdentifier identifier)
    {
        connectionId = ConnectionId;
        identifier = Identifier;
    }
}