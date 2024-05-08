using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Shared.Extensions;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Stations.Models;
public record StationResponse : BaseAuditableEntityResponse<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public double Balance { get; set; }
    public UserResponse? Manager => Users.FirstOrDefault(u => u.Roles.Any(_ => _.Name == RoleName.StationManager)) ?? null;
    public string FormatBalance => Balance.FormatMoney();
    // TODO: them fiel rack,slot,..
    public ICollection<StationImageResponse> StationImages { get; set; } = new HashSet<StationImageResponse>();
    public ICollection<PricingResponse> Pricings { get; set; } = new HashSet<PricingResponse>();

    [JsonIgnore]
    public ICollection<UserResponse> Users { get; set; } = new HashSet<UserResponse>();

    //public ICollection<PaymentResponse> Payments { get; set; } = new HashSet<PaymentResponse>();
}
