using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Station : BaseAuditableEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public bool IsActive { get; set; }

    public double Balance { get; set; }

    [Projectable]
    [NotMapped]// phai NotMapped nếu không thì EF sẽ tưỡng là station có nhiều user và tạo cột stationId trong bảng user, lúc query sẽ bị lỗi
    public IEnumerable<User> Users => UserStations.Select(us => us.User);

    public virtual ICollection<UserStation> UserStations { get; set; } = new HashSet<UserStation>();
    public virtual ICollection<StationImage> StationImages { get; set; } = new HashSet<StationImage>();
    public virtual ICollection<Zone> Zones { get; set; } = new HashSet<Zone>();
    public virtual ICollection<Pricing> Pricings { get; set; } = new HashSet<Pricing>();

    public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

}
