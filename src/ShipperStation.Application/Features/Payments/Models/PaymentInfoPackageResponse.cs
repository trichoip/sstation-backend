using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Payments.Models;
public sealed record PaymentInfoPackageResponse : BaseAuditableEntityResponse<int>
{
    public string? Description { get; set; }
    public double ServiceFee { get; set; }
    public double TotalPrice { get; set; }
    public PaymentType Type { get; set; }
    public PaymentStatus Status { get; set; }

    public Guid PackageId { get; set; }
    public int StationId { get; set; }
}
