using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Devices.Commands;
public sealed record DeleteDeviceByTokenCommand(string Token) : IRequest<MessageResponse>;