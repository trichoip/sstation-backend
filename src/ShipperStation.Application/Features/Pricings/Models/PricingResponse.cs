using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Pricings.Models;
public sealed record PricingResponse
{
    public int Id { get; set; }
    public int FromDate { get; set; }
    public int ToDate { get; set; }
    public double Price { get; set; }
    public string FormatPrice => Price.FormatMoney();
}
