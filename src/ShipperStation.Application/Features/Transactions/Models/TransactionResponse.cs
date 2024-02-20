using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Transactions.Models;
public sealed record TransactionResponse : BaseAuditableEntityResponse<Guid>
{
    public string? Description { get; set; }
    public double Amount { get; set; }
    public TransactionStatus Status { get; set; }
    public TransactionType Type { get; set; }
    public string Url { get; set; } = default!;
    public TransactionMethod Method { get; set; }
}
