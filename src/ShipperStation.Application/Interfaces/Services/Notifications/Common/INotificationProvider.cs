using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Interfaces.Services.Notifications.Common;

public interface INotificationProvider
{
    void Attach(NotificationType type, INotificationService notificationService);

    void Attach(ICollection<NotificationType> types, INotificationService notificationService);

    void Attach(NotificationType type, ICollection<INotificationService> notificationServices);

    void Detach(NotificationType type, INotificationService notificationService);

    Task NotifyAsync(Notification notification);
}