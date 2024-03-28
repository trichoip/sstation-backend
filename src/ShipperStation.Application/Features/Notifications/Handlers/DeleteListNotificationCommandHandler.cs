using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Notifications.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Notifications.Handlers;
internal sealed class DeleteListNotificationCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<DeleteListNotificationCommand, MessageResponse>
{
    private readonly IGenericRepository<Notification> _notificationRepository = unitOfWork.Repository<Notification>();
    public async Task<MessageResponse> Handle(DeleteListNotificationCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        foreach (var id in request.Ids)
        {
            var notification = await _notificationRepository
                .FindByAsync(
                _ => _.Id == id &&
                     _.UserId == userId,
                cancellationToken: cancellationToken);

            if (notification == null)
            {
                throw new NotFoundException(nameof(Notification), id);
            }

            await _notificationRepository.DeleteAsync(notification);
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.NotificationDeletedSuccess);
    }
}
