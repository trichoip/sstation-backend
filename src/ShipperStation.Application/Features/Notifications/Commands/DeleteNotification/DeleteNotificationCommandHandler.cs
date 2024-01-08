using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Notifications.Commands.DeleteNotification;
internal sealed class DeleteNotificationCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<DeleteNotificationCommand, MessageResponse>
{

    private readonly IGenericRepository<Notification> _notificationRepository = unitOfWork.Repository<Notification>();

    public async Task<MessageResponse> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var notification = await _notificationRepository
            .FindByAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

        if (notification == null)
        {
            throw new NotFoundException(nameof(Notification), request.Id);
        }

        if (notification.UserId != userId)
        {
            throw new NotFoundException(Resource.UserNotHaveNotification.Format(userId, request.Id));
        }

        await _notificationRepository.DeleteAsync(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.NotificationDeletedSuccess);
    }
}
