using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Domain.Constants;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Stations.Models;
public record StationAllResponse : StationResponse
{
    public UserResponse? Manager => Users.FirstOrDefault(u => u.Roles.Any(_ => _.Name == RoleName.StationManager)) ?? null;
    [JsonIgnore]
    public ICollection<UserResponse> Users { get; set; } = new HashSet<UserResponse>();

    //public ICollection<PaymentResponse> Payments { get; set; } = new HashSet<PaymentResponse>();
}
