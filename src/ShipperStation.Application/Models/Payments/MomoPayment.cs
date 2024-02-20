namespace ShipperStation.Application.Models.Payments;

public class MomoPayment
{
    public string PaymentReferenceId { get; set; } = default!;

    public long Amount { get; set; }

    public string? Info { get; set; }

    public string returnUrl { get; set; } = default!;
}