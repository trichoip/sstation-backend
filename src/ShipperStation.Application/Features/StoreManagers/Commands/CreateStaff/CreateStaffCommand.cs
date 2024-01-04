using MediatR;
using ShipperStation.Application.Contracts;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStaff;
public sealed record CreateStaffCommand : IRequest<MessageResponse>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? FullName { get; set; }

    [JsonIgnore]
    public int StationId { get; set; }
}
