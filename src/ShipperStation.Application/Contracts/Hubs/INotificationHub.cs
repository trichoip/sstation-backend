using ShipperStation.Application.Models.Notifications;

namespace ShipperStation.Application.Contracts.Hubs;
public interface INotificationHub
{
    Task ReceiveNotification(NotificationRequest notification);
}
