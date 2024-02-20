using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Models.Payments;

public class VnPayPayment
{
    public string PaymentReferenceId { get; set; } = default!;

    public long Amount { get; set; }

    public string? Info { get; set; }

    public TransactionType OrderType { get; set; }

    public DateTimeOffset Time { get; set; }

    public string returnUrl { get; set; } = default!;
}