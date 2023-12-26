using ShipperStation.Application.Contracts.Notifications;

namespace ShipperStation.Application.Interfaces.Hubs;
public interface INotificationHub
{
    Task ReceiveNotification(NotificationRequest notification);
}
