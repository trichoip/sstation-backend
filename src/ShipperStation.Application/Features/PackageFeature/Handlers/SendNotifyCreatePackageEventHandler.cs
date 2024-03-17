using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class SendNotifyCreatePackageEventHandler(INotifier notifier, IUnitOfWork unitOfWork) : INotificationHandler<SendNotifyCreatePackageEvent>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task Handle(SendNotifyCreatePackageEvent notification, CancellationToken cancellationToken)
    {
        var notificationReceiver = new NotificationRequest
        {
            Type = NotificationType.CustomerPackageCreatedReceiverApp,
            UserId = notification.ReceiverId,
        };

        if (!await _deviceRepository.ExistsByAsync(_ => _.UserId == notification.ReceiverId, cancellationToken))
        {
            var userReceiver = await _userRepository.FindByIdAsync(notification.ReceiverId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), notification.ReceiverId);

            notificationReceiver.Type = NotificationType.CustomerPackageCreatedReceiverSms;
            notificationReceiver.PhoneNumber = userReceiver.PhoneNumber;

        }

        await notifier.NotifyAsync(notificationReceiver, true, cancellationToken);

        var notificationSender = new NotificationRequest
        {
            Type = NotificationType.CustomerPackageCreatedSenderApp,
            UserId = notification.SenderId,
        };

        if (!await _deviceRepository.ExistsByAsync(_ => _.UserId == notification.SenderId, cancellationToken))
        {
            var userSender = await _userRepository.FindByIdAsync(notification.SenderId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), notification.SenderId);

            notificationReceiver.Type = NotificationType.CustomerPackageCreatedSenderSms;
            notificationReceiver.PhoneNumber = userSender.PhoneNumber;
        }

        await notifier.NotifyAsync(notificationSender, true, cancellationToken);

    }
}
