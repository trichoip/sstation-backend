using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Notifications.Commands;
public sealed record DeleteNotificationCommand(int Id) : IRequest<MessageResponse>;

