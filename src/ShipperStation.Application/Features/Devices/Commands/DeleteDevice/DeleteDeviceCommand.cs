using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Devices.Commands.DeleteDevice;
public sealed record DeleteDeviceCommand(int Id) : IRequest<MessageResponse>;
