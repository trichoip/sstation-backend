using ShipperStation.Application.Features.Users.Models;

namespace ShipperStation.Application.Features.UserStations.Models;
public sealed record UserStationResponse
{
    public UserResponse User { get; set; } = default!;
}
