namespace ShipperStation.Application.Contracts.Pricings;
public sealed record PricingResponse
{
    public int Id { get; set; }
    public int FromDate { get; set; }
    public int ToDate { get; set; }
    public double Price { get; set; }
}
