using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Devices.Commands.CreateDevice;
public sealed record CreateDeviceCommand : IRequest<MessageResponse>
{
    public string Token { get; set; } = default!;
}

