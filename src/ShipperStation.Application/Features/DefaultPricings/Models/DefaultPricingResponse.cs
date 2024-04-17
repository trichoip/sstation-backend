using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.DefaultPricings.Models;
public sealed record DefaultPricingResponse
{
    public int Id { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public string DurationTime => $"{EndTime - StartTime} h";
    public double PricePerUnit { get; set; }
    public string FormatPricePerUnit => PricePerUnit.FormatMoney();
    public double UnitDuration { get; set; }
    public string UnitDurationMinutes => $"{UnitDuration * 60} min";

    public string FormatUnitDuration => $"{UnitDuration} h";

}