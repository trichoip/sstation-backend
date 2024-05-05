using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Payment : BaseAuditableEntity<int>
{
    public string? Description { get; set; }
    public double ServiceFee { get; set; }
    public double TotalPrice { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public PaymentStatus Status { get; set; }

    public Guid PackageId { get; set; }
    public virtual Package Package { get; set; } = default!;

    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;
}
