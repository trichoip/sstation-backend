using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Hubs;
public interface INotificationHub
{
    Task ReceiveNotification(Notification notification);
}
