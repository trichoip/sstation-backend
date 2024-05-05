using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Pricings.Models;
public sealed record PricingResponse
{
    public int Id { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public double Price { get; set; }
    public string FormatPrice => Price.FormatMoney();
    public int StationId { get; set; }
}
