namespace ShipperStation.Application.Contracts.Stations;
public sealed record StationImageResponse : BaseAuditableEntityResponse<int>
{
    public string ImageUrl { get; set; } = default!;
}
