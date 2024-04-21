using ShipperStation.Application.Extensions;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Shared.Extensions;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record QrPaymentPackage
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double PriceCod { get; set; }
    [JsonIgnore]
    public double Volume { get; set; }
    [JsonIgnore]
    public double TotalHours { get; set; }
    public double TotalPrice => PriceCod + ServiceFee;
    public double ServiceFee => Pricing is null ? 0 : PackageExtensions.CalculateServiceFee(Volume, TotalHours, Pricing.PricePerUnit, Pricing.UnitDuration);
    public string FormatTotalPrice => TotalPrice.FormatMoney();
    public string FormatServiceFee => ServiceFee.FormatMoney();

    [JsonIgnore]
    public PricingResponse? Pricing => Pricings.Where(_ => _.StartTime <= TotalHours && _.EndTime >= TotalHours).FirstOrDefault();

    [JsonIgnore]
    public ICollection<PricingResponse> Pricings { get; set; } = new HashSet<PricingResponse>();
}
