using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Staffs.Commands;
public sealed record UpdateStaffCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public Guid StaffId { get; set; }

    [JsonIgnore]
    public int StationId { get; set; }

    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
}

