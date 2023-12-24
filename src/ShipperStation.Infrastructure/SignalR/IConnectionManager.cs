namespace ShipperStation.Infrastructure.SignalR;

public interface IConnectionManager
{
    void KeepConnection(Guid accId, string connectionId);

    void RemoveConnection(string connectionId);

    List<string> GetConnections(Guid accId);
}