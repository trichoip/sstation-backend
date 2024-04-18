using Mapster;
using Microsoft.Extensions.Logging;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Extensions;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using INotificationService = ShipperStation.Application.Contracts.Services.Notifications.INotificationService;

namespace ShipperStation.Infrastructure.Services.Notifications;

public class Notifier : INotifier
{
    private readonly INotificationProvider _provider;
    private readonly ILogger<Notifier> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public Notifier(
        INotificationProvider provider,
        ISignalRNotificationService signalRNotificationService,
        IFirebaseNotificationService firebaseNotificationService,
        ISmsNotificationService smsNotificationService,
        ILogger<Notifier> logger,
        IUnitOfWork unitOfWork)
    {
        _provider = provider;
        _logger = logger;
        _unitOfWork = unitOfWork;

        _provider.Attach(NotificationType.VerificationCode, new List<INotificationService>()
        {
            smsNotificationService
        });

        _provider.Attach(NotificationType.SystemStaffCreated, new List<INotificationService>()
        {
            firebaseNotificationService,
            signalRNotificationService,
        });

        _provider.Attach(new List<NotificationType>()
        {
            NotificationType.CustomerPackageCanceled,
            NotificationType.CustomerPaymentPackage,
            NotificationType.CustomerPackageCreated,
            NotificationType.CustomerPackageCompleted,
            NotificationType.CustomerPackageReturned,
            NotificationType.PackageExprire,
            NotificationType.PackageReceive,
        }, firebaseNotificationService);

        _provider.Attach(new List<NotificationType>()
        {
            NotificationType.NotiPackagePaymentSuccessForStaff,
        }, signalRNotificationService);

    }

    public async Task NotifyAsync(
        NotificationRequest notificationRequset,
        bool isSaved = true,
        CancellationToken cancellationToken = default)
    {
        notificationRequset.InitNotification();

        if (isSaved)
        {
            var notification = notificationRequset.Adapt<Notification>();
            await _unitOfWork.Repository<Notification>().CreateAsync(notification, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            notification.Adapt(notificationRequset);
        }

        _logger.LogInformation($"[PUSH NOTIFICATION]: {notificationRequset}");
        await _provider.NotifyAsync(notificationRequset, cancellationToken);
    }
}