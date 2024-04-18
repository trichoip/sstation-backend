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

            case NotificationType.CustomerPackageCreated:
                notification.Title = NotificationType.CustomerPackageCreated.GetDescription();
                notification.Content = "You have a new package";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.CustomerPaymentPackage:
                notification.Title = NotificationType.CustomerPaymentPackage.GetDescription();
                notification.Content = "The package you sent has been paid";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.CustomerPackageCanceled:
                notification.Title = NotificationType.CustomerPackageCanceled.GetDescription();
                notification.Content = "The package you have has been canceled";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.CustomerPackageCompleted:
                notification.Title = NotificationType.CustomerPackageCompleted.GetDescription();
                notification.Content = "The package has been completed";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.CustomerPackageReturned:
                notification.Title = NotificationType.CustomerPackageReturned.GetDescription();
                notification.Content = "The package has been returned";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.PackageExprire:
                notification.Title = NotificationType.PackageExprire.GetDescription();
                notification.Content = "The package has been expired";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.PackageReceive:
                notification.Title = NotificationType.PackageReceive.GetDescription();
                notification.Content = "Package needs to be received";
                notification.Level = NotificationLevel.Information;
                break;

            case NotificationType.NotiPackagePaymentSuccessForStaff:
                notification.Title = NotificationType.NotiPackagePaymentSuccessForStaff.GetDescription();
                notification.Content = "Package success payment";
                notification.Level = NotificationLevel.Information;
                break;

            default:
                throw new ApplicationException(Resource.NotificationNotSupported.Format(notification.Type));
        };

    }
}