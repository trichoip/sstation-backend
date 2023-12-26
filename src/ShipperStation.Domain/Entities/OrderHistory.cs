using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class OrderHistory : BaseAuditableEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public OrderStatus? Status { get; set; }

    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = default!;
}
