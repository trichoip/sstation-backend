using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Notifications.Models;
using ShipperStation.Application.Features.Notifications.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Notifications.Handlers;
internal sealed class GetNotificationByIdQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetNotificationByIdQuery, NotificationResponse>
{
    private readonly IGenericRepository<Notification> _notificationRepository = unitOfWork.Repository<Notification>();

    public async Task<NotificationResponse> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var notification = await _notificationRepository
            .FindByAsync<NotificationResponse>(
            _ => _.Id == request.Id &&
                 _.UserId == userId,
            cancellationToken);

        if (notification == null)
        {
            throw new NotFoundException(nameof(Notification), request.Id);
        }

        return notification;
    }
}
