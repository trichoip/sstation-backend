using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Devices.Commands;
public sealed record CreateDeviceCommand : IRequest<MessageResponse>
{
    public string Token { get; set; } = default!;
}

