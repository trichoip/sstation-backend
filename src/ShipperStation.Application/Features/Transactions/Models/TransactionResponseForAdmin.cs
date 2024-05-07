using ShipperStation.Application.Features.Payments.Models;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Transactions.Models;
public sealed record TransactionResponseForAdmin : BaseAuditableEntityResponse<Guid>
{
    public string? Description { get; set; }
    public double Amount { get; set; }
    public TransactionStatus Status { get; set; }
    public TransactionType Type { get; set; }
    public string Url { get; set; } = default!;
    public TransactionMethod Method { get; set; }
    public Guid UserId { get; set; }

    public UserResponse User { get; set; } = default!;

    public PaymentInfoTransactionResponse Payment { get; set; } = default!;
}
