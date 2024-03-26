using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Notifications.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Notifications.Handlers;
internal sealed class ReadAllNotificationCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<ReadAllNotificationCommand, MessageResponse>
{
    private readonly IGenericRepository<Notification> _notificationRepository = unitOfWork.Repository<Notification>();
    public async Task<MessageResponse> Handle(ReadAllNotificationCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var notifications = await _notificationRepository
            .FindAsync(
                _ => _.UserId == userId && _.IsRead == false,
            isAsNoTracking: false,
            cancellationToken: cancellationToken);

        foreach (var n in notifications)
        {
            n.ReadAt = DateTimeOffset.UtcNow;
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.NotificationStatusUpdatedSuccess);
    }
}
