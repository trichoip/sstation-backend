using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record PackageResponse : BaseAuditableEntityResponse<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double PriceCod { get; set; }
    public bool IsCod { get; set; }
    public string? Barcode { get; set; }
    public PackageStatus Status { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    public int SlotId { get; set; }

    public Guid SenderId { get; set; }
    //public User Sender { get; set; } = default!;

    public Guid ReceiverId { get; set; }
    //public User Receiver { get; set; } = default!;

    //public  ICollection<PackageImage> PackageImages { get; set; } = new HashSet<PackageImage>();

    //public  ICollection<PackageStatusHistory> PackageStatusHistories { get; set; } = new HashSet<PackageStatusHistory>();

    public StationResponse Station { get; set; } = default!;
}
