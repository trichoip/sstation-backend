using CorePush.Firebase;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Infrastructure.Settings;

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
        var deviceIds = (await _unitOfWork.Repository<UserToken>()
            .FindAsync(expression: token => token.UserId == notification.UserId, cancellationToken: cancellationToken))
            .Select(_ => _.Value);

        var settings = new FirebaseSettings(
            _fcmSettings.ProjectId,
            _fcmSettings.PrivateKey,
            _fcmSettings.ClientEmail,
            _fcmSettings.TokenUri);

        var fcmSender = new FirebaseSender(settings, new HttpClient());

        foreach (var token in deviceIds)
        {
            try
            {
                var firebaseNotification = new
                {
                    message = new
                    {
                        token,
                        notification = new
                        {
                            title = notification.Title,
                            body = notification.Content
                        },
                        data = new
                        {
                            type = notification.Type.ToString(),
                            //entityType = notification.EntityType.ToString(),
                            notification.ReferenceId
                        },
                    }
                };

                await fcmSender.SendAsync(firebaseNotification, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[MOBILE NOTIFICATION] Error when push notification: {exception.Message}");
            }
        }
        _logger.LogInformation($"[[MOBILE NOTIFICATION]] Handle firebase notification: {notification.Id}");
    }
}