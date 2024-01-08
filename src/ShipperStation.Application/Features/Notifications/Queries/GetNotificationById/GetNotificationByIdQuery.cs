using MediatR;
using ShipperStation.Application.Contracts.Notifications;

namespace ShipperStation.Application.Features.Notifications.Queries.GetNotificationById;
public sealed record GetNotificationByIdQuery(int Id) : IRequest<NotificationResponse>;
