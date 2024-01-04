namespace ShipperStation.Application.Contracts.Zones;
public sealed record ZoneResponse : BaseAuditableEntityResponse<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
