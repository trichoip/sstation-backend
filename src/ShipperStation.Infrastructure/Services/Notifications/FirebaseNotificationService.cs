using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities;
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

        var deviceIds = (await _unitOfWork.Repository<Device>()
            .FindAsync(
                expression: token => token.UserId == notification.UserId,
                cancellationToken: cancellationToken))
            .Select(_ => _.Token).ToList();

        var messages = new MulticastMessage()
        {
            Tokens = deviceIds,
            Data = new Dictionary<string, string>(),
            Notification = new FirebaseAdmin.Messaging.Notification()
            {
                Title = notification.Title,
                Body = notification.Content
            }
        };

        var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(messages, cancellationToken);

        //var settings = new FirebaseSettings(
        //    _fcmSettings.ProjectId,
        //    _fcmSettings.PrivateKey,
        //    _fcmSettings.ClientEmail,
        //    _fcmSettings.TokenUri);

        //var fcmSender = new FirebaseSender(settings, new HttpClient());

        foreach (var token in deviceIds)
        {
            try
            {

                //var message = new Message()
                //{
                //    Token = token,
                //    Data = new Dictionary<string, string>(),
                //    Notification = new Notification()
                //    {
                //        Title = notification.Title,
                //        Body = notification.Content
                //    }
                //};

                //string response = await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);

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
                            //notification.ReferenceId
                        },
                    }
                };

                //await fcmSender.SendAsync(firebaseNotification, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[MOBILE NOTIFICATION] Error when push notification: {exception.Message}");
            }
        }
        _logger.LogInformation($"[[MOBILE NOTIFICATION]] Handle firebase notification: {notification.Id}");
    }
}