using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.DefaultPricings.Models;
public sealed record DefaultPricingResponse
{
    public int Id { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public double Price { get; set; }
    public string FormatPrice => Price.FormatMoney();
}