using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Notifications.Queries.GetNotificationById;
internal sealed class GetNotificationByIdQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetNotificationByIdQuery, NotificationResponse>
{
    private readonly IGenericRepository<Notification> _notificationRepository = unitOfWork.Repository<Notification>();

    public async Task<NotificationResponse> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var notification = await _notificationRepository
            .FindByAsync<NotificationResponse>(_ => _.Id == request.Id, cancellationToken);

        if (notification == null)
        {
            throw new NotFoundException(nameof(Notification), request.Id);
        }

        if (notification.UserId != userId)
        {
            throw new NotFoundException(Resource.UserNotHaveNotification.Format(userId, request.Id));
        }

        return notification;
    }
}
