using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Payments.Models;
public sealed record PaymentResponse : BaseAuditableEntityResponse<int>
{
    public string? Description { get; set; }
    public double ServiceFee { get; set; }
    public double TotalPrice { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentType Type { get; set; }

    public Guid PackageId { get; set; }
    public int StationId { get; set; }
    public Guid? TransactionId { get; set; }

    public StationResponse Station { get; set; } = default!;

    public PackageResponseOfStatusHistory Package { get; set; } = default!;
    public TransactionResponse Transaction { get; set; } = default!;
}
