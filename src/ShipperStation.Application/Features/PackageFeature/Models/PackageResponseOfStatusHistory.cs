using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record PackageResponseOfStatusHistory : BaseAuditableEntityResponse<Guid>
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

    public int NotificationCount { get; set; }
    public string? Reason { get; set; }

    public int CheckinDays => DateTimeOffset.UtcNow.Subtract(CreatedAt.Value).Days;

    public string Location { get; set; } = default!;

    public double TotalHours { get; set; }

    public ICollection<PackageImageResponse> PackageImages { get; set; } = new HashSet<PackageImageResponse>();

}
