namespace ShipperStation.Application.Features.DefaultPricings.Models;
public sealed record DefaultPricingResponse
{
    public int Id { get; set; }
    public int FromDate { get; set; }
    public int ToDate { get; set; }
    public double Price { get; set; }
}
