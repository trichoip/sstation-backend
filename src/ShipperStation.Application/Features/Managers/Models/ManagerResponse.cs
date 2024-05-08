using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Users.Models;

namespace ShipperStation.Application.Features.Managers.Models;
public sealed record ManagerResponse : UserResponse
{
    public ICollection<StationResponse> Stations { get; set; } = new HashSet<StationResponse>();
}
