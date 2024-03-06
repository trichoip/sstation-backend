using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Notifications.Models;
using ShipperStation.Application.Features.Notifications.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Notifications.Handlers;
internal sealed class GetNotificationsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetNotificationsQuery, NotificationPaginatedResponse>
{
    private readonly IGenericRepository<Notification> _notificationRepository = unitOfWork.Repository<Notification>();
    public async Task<NotificationPaginatedResponse> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        request = request with
        {
            UserId = userId,
            SortDir = SortDirection.Desc,
            SortColumn = nameof(Notification.CreatedAt)
        };

        var notifications = await _notificationRepository
            .FindAsync<NotificationResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        var countUnread = await _notificationRepository
            .CountAsync(_ => _.UserId == userId && !_.IsRead, cancellationToken);

        return new NotificationPaginatedResponse(notifications, countUnread);
    }
}
