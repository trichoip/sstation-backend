using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Extensions;
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

            case NotificationType.CustomerPackageCreatedReceiverApp:
                notification.Title = NotificationType.CustomerPackageCreatedReceiverSms.GetDescription();
                notification.Content = "You have a new package";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.CustomerPackageCreatedReceiverSms:
                notification.Title = NotificationType.CustomerPackageCreatedReceiverSms.GetDescription();
                notification.Content = "You have a new package, please go to the app to see it";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.CustomerPackageCreatedSenderApp:
                notification.Title = NotificationType.CustomerPackageCreatedSenderApp.GetDescription();
                notification.Content = "You have sent a package";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.CustomerPackageCreatedSenderSms:
                notification.Title = NotificationType.CustomerPackageCreatedSenderSms.GetDescription();
                notification.Content = "You have sent a package, please go to the app to see it";
                notification.Level = NotificationLevel.Information;
                break;

            default:
                throw new ApplicationException(Resource.NotificationNotSupported.Format(notification.Type));
        };

    }
}