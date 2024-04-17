using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Users.Commands;
public sealed record DeleteUserForAdminCommand(Guid Id) : IRequest<MessageResponse>;