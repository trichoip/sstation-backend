namespace ShipperStation.Application.Contracts.Stations;
public sealed record StationImageResponse : BaseEntityResponse<int>
{
    public string ImageUrl { get; set; } = default!;
}
