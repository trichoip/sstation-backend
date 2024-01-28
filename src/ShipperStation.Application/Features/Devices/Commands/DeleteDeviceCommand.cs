using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Devices.Commands;
public sealed record DeleteDeviceCommand(int Id) : IRequest<MessageResponse>;
