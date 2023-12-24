using CorePush.Firebase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Infrastructure.Settings;

namespace ShipperStation.Infrastructure.Services.Notifications.Mobile.Firebase;

public class FirebaseNotificationService : IMobileNotificationService
{
    private readonly ILogger<FirebaseNotificationService> _logger;

    private readonly FcmSettings _fcmSettings;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FirebaseNotificationService(
        ILogger<FirebaseNotificationService> logger,
        IOptions<FcmSettings> fcmSettings,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _fcmSettings = fcmSettings.Value;
        _serviceScopeFactory = serviceScopeFactory;

    }

    public async Task NotifyAsync(Notification notification)
    {
        // get device ids
        using var scope = _serviceScopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var notiAdapter = scope.ServiceProvider.GetRequiredService<INotificationAdapter>();

        //var deviceIds = await unitOfWork.TokenRepository
        //    .Get(token => token.AccountId == notification.AccountId
        //                  && Equals(TokenType.DeviceToken, token.Type)
        //                  && Equals(TokenStatus.Valid, token.Status))
        //    .ToListAsync();

        int[] deviceIds = new int[] { 1, 2, 3, 4, 5 };

        _logger.LogInformation("Firebase key: {0}", _fcmSettings.PrivateKey);

        var settings = new FirebaseSettings(_fcmSettings.ProjectId, _fcmSettings.PrivateKey, _fcmSettings.ClientEmail, _fcmSettings.TokenUri);
        var fcmSender = new FirebaseSender(settings, new HttpClient());

        foreach (var token in deviceIds)
        {
            try
            {
                //var firebaseNotification = await notiAdapter.ToFirebaseNotification(notification, token.Value);
                //await fcmSender.SendAsync(firebaseNotification);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[MOBILE NOTIFICATION] Error when push notification: {exception.Message}");
            }
        }
        _logger.LogInformation("[[MOBILE NOTIFICATION]] Handle firebase notification: {0}", notification.Id);
    }
}