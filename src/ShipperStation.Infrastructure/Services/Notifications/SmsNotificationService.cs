using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Infrastructure.Services.Notifications;
public class SmsNotificationService : ISmsNotificationService
{
    private readonly ISmsSender _smsSender;
    private readonly IUnitOfWork _unitOfWork;
    public SmsNotificationService(
        ISmsSender smsSender,
        IUnitOfWork unitOfWork)
    {
        _smsSender = smsSender;
        _unitOfWork = unitOfWork;
    }

    public async Task NotifyAsync(NotificationRequest notification, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Repository<User>()
            .FindByAsync(_ => _.Id == notification.UserId, cancellationToken: cancellationToken);
        if (user is null || user.PhoneNumber is null)
        {
            throw new NotFoundException(nameof(User), notification.UserId);
        }

        await _smsSender.SendAsync(user.PhoneNumber, notification.Content, cancellationToken);
    }
}
