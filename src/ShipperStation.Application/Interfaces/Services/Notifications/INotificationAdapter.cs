using ShipperStation.Application.Interfaces.Services.Notifications.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Interfaces.Services.Notifications;

public interface INotificationAdapter
{
    public Task<WebNotification> ToWebNotification(Notification notification, string connectionId);

    public Task<ZaloZnsNotification> ToZaloZnsNotification(Notification notification);

    public Task<FirebaseNotification> ToFirebaseNotification(Notification notification, string deviceToken, DeviceType? deviceType = null);
}