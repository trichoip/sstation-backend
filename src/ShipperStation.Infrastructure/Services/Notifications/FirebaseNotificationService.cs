using CorePush.Firebase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Application.Interfaces.Services.Notifications.Common;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Infrastructure.Settings;

namespace ShipperStation.Infrastructure.Services.Notifications;

public class FirebaseNotificationService : IFirebaseNotificationService
{
    private readonly ILogger<FirebaseNotificationService> _logger;
    private readonly FcmSettings _fcmSettings;
    private readonly UserManager<User> _userManager;
    private readonly INotificationAdapter _notificationAdapter;
    private readonly IUnitOfWork _unitOfWork;

    public FirebaseNotificationService(
        ILogger<FirebaseNotificationService> logger,
        IOptions<FcmSettings> fcmSettings,
        UserManager<User> userManager,
        INotificationAdapter notificationAdapter,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _fcmSettings = fcmSettings.Value;
        _userManager = userManager;
        _notificationAdapter = notificationAdapter;
        _unitOfWork = unitOfWork;
    }

    public async Task NotifyAsync(Notification notification)
    {

        var deviceIds = (await _unitOfWork.Repository<UserToken>()
            .FindAsync(expression: token =>
                token.UserId == notification.UserId &&
                token.Type == TokenType.DeviceToken &&
                token.Status == TokenStatus.Valid))
            .Select(_ => _.Value);

        _logger.LogInformation("Firebase key: {0}", _fcmSettings.PrivateKey);

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
                var firebaseNotification = await _notificationAdapter.ToFirebaseNotification(notification, token);
                await fcmSender.SendAsync(firebaseNotification);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[MOBILE NOTIFICATION] Error when push notification: {exception.Message}");
            }
        }
        _logger.LogInformation("[[MOBILE NOTIFICATION]] Handle firebase notification: {0}", notification.Id);
    }
}