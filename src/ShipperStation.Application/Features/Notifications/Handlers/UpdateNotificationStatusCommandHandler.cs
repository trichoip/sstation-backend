using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Notifications.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Notifications.Handlers;
internal sealed class UpdateNotificationStatusCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<UpdateNotificationStatusCommand, MessageResponse>
{
    private readonly IGenericRepository<Notification> _notificationRepository = unitOfWork.Repository<Notification>();
    public async Task<MessageResponse> Handle(UpdateNotificationStatusCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var notification = await _notificationRepository
            .FindByAsync(
            _ => _.Id == request.Id &&
                 _.UserId == userId,
            cancellationToken: cancellationToken);

        if (notification == null)
        {
            throw new NotFoundException(nameof(Notification), request.Id);
        }

        notification.ReadAt = request.IsRead ? DateTimeOffset.UtcNow : null;

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.NotificationStatusUpdatedSuccess);
    }
}
