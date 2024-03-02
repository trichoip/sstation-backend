using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Station : BaseAuditableEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public virtual ICollection<UserStation> UserStations { get; set; } = new HashSet<UserStation>();
    public virtual ICollection<StationImage> StationImages { get; set; } = new HashSet<StationImage>();
    public virtual ICollection<Zone> Zones { get; set; } = new HashSet<Zone>();
    public virtual ICollection<Pricing> Pricings { get; set; } = new HashSet<Pricing>();

}
