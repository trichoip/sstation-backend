using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Notifications.Commands.DeleteNotification;
public sealed record DeleteNotificationCommand(int Id) : IRequest<MessageResponse>;

