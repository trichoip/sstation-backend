using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Common.Extensions;
public static class NotificationExtensions
{
    public static void InitNotification(this NotificationRequest notification)
    {
        switch (notification.Type)
        {
            case NotificationType.VerificationCode:
                notification.Content = Resource.NotificationContentOtpMessage.Format(notification.Data ?? string.Empty);
                break;

            case NotificationType.SystemStaffCreated:
                notification.Title = "Test push Title";
                notification.Content = "Test push Content";
                notification.Level = NotificationLevel.Information;
                break;
            default:
                throw new ApplicationException(Resource.NotificationNotSupported.Format(notification.Type));
        };

    }
}