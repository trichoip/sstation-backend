using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Infrastructure.Settings;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Infrastructure.Services.Notifications;

public class FirebaseNotificationService : IFirebaseNotificationService
{
    private readonly ILogger<FirebaseNotificationService> _logger;
    private readonly FcmSettings _fcmSettings;
    private readonly IUnitOfWork _unitOfWork;

    public FirebaseNotificationService(
        ILogger<FirebaseNotificationService> logger,
        IOptions<FcmSettings> fcmSettings,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _fcmSettings = fcmSettings.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task NotifyAsync(NotificationRequest notification, CancellationToken cancellationToken = default)
    {
        var deviceIds = (await _unitOfWork.Repository<Device>()
            .FindAsync(
                expression: token => token.UserId == notification.UserId,
                cancellationToken: cancellationToken))
            .Select(_ => _.Token).ToList();

        var messages = new MulticastMessage()
        {
            Tokens = deviceIds,
            Data = notification.ObjectToDictionary(),
            Notification = new FirebaseAdmin.Messaging.Notification()
            {
                Title = notification.Title,
                Body = notification.Content
            }
        };

        try
        {
            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(messages, cancellationToken);
            _logger.LogInformation($"[MOBILE NOTIFICATION] Success push notification: {response}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"[MOBILE NOTIFICATION] Error when push notification: {ex.Message}");
        }

        _logger.LogInformation($"[[MOBILE NOTIFICATION]] Handle firebase notification: {notification.Id}");
    }
}