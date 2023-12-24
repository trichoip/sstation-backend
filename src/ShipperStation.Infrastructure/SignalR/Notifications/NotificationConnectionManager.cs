namespace ShipperStation.Infrastructure.SignalR.Notifications;

public class NotificationConnectionManager : IConnectionManager
{
    private static Dictionary<Guid, List<string>> _connections = new();

    public void KeepConnection(Guid accId, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.ContainsKey(accId))
            {
                _connections.Add(accId, new());
            }
            _connections[accId].Add(connectionId);
        }
    }

    public void RemoveConnection(string connectionId)
    {
        lock (_connections)
        {
            foreach (var accId in _connections.Keys)
            {
                if (_connections[accId].Contains(connectionId))
                {
                    _connections[accId].Remove(connectionId);
                    break;
                }
            }
        }
    }

    public List<string> GetConnections(Guid accId)
    {
        List<string> connections = new();
        lock (_connections)
        {
            if (_connections.ContainsKey(accId))
            {
                connections = _connections[accId];
            }
        }
        return connections;
    }
}