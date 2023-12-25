using Microsoft.Extensions.Logging;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Application.Interfaces.Services.Notifications.Common;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Helpers;
using INotificationService = ShipperStation.Application.Interfaces.Services.Notifications.Common.INotificationService;

namespace ShipperStation.Infrastructure.Services.Notifications.Common;

public class Notifier : INotifier
{
    private readonly INotificationProvider _provider;
    private readonly ILogger<Notifier> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public Notifier(
        INotificationProvider provider,
        ISignalRNotificationService signalRNotificationService,
        IFirebaseNotificationService firebaseNotificationService,
        IZaloNotificationService zaloNotificationService,
        ICallerNotificationService callerNotificationService,
        ISmsNotificationService smsNotificationService,
        ILogger<Notifier> logger,
        IUnitOfWork unitOfWork)
    {
        _provider = provider;
        _logger = logger;
        _unitOfWork = unitOfWork;

        _provider.Attach(NotificationType.AccountOtpCreated, new List<INotificationService>()
        {
            signalRNotificationService,
            firebaseNotificationService,
            zaloNotificationService,
            callerNotificationService,
            smsNotificationService
        });

    }

    public async Task NotifyAsync(Notification notification)
    {
        if (notification.Saved)
        {
            await _unitOfWork.Repository<Notification>().CreateAsync(notification);
            await _unitOfWork.CommitAsync();
        }

        _logger.LogInformation("[PUSH NOTIFICATION]: {0}", JsonSerializerUtils.Serialize(notification));
        await _provider.NotifyAsync(notification);
    }
}