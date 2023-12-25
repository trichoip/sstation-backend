using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Interfaces.Services.Notifications.Common;

public interface INotificationAdapter
{
    public Task<WebNotification> ToWebNotification(Notification notification, string connectionId);

    public Task<ZaloZnsNotification> ToZaloZnsNotification(Notification notification);

    public Task<FirebaseNotification> ToFirebaseNotification(Notification notification, string deviceToken, DeviceType? deviceType = null);
}