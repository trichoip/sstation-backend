using MediatR;
using ShipperStation.Application.Contracts;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Devices.Commands.UpdateDevice;
public sealed record UpdateDeviceCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Token { get; set; } = default!;
}
