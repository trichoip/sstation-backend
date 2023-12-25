using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Contracts.Payments;

public class VnPayPayment
{

    public string PaymentReferenceId { get; set; } = default!;

    public string OrderReferenceId { get; set; } = default!;

    public long Amount { get; set; }

    public string? Info { get; set; }

    public OrderType OrderType { get; set; }

    public DateTimeOffset Time { get; set; }

}