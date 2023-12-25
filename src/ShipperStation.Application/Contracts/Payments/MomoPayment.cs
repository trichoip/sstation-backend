namespace ShipperStation.Application.Contracts.Payments;

public class MomoPayment
{
    public string PaymentReferenceId { get; set; } = default!;

    public string OrderReferenceId { get; set; } = default!;

    public long Amount { get; set; }

    public string? Info { get; set; }
}