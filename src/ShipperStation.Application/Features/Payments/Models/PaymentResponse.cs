using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Payments.Models;
public sealed record PaymentResponse : BaseAuditableEntityResponse<int>
{
    public double ServiceFee { get; set; }
    public double PriceCod { get; set; }
    public double TotalPrice { get; set; }
    public PaymentStatus Status { get; set; }

    public Guid PackageId { get; set; }
    public int StationId { get; set; }
}
