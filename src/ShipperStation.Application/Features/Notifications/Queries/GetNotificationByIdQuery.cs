using MediatR;
using ShipperStation.Application.Features.Notifications.Models;

namespace ShipperStation.Application.Features.Notifications.Queries;
public sealed record GetNotificationByIdQuery(int Id) : IRequest<NotificationResponse>;
