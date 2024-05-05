using ShipperStation.Application.Extensions;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Shared.Extensions;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record QrPaymentPackage
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public double Volume { get; set; }

    [JsonIgnore]
    public double TotalDays { get; set; }
    public double TotalPrice => ServiceFee;
    public double ServiceFee => Pricing is null ? 1000 : PackageExtensions.CalculateServiceFee(Volume, TotalDays, Pricing.Price);
    public string FormatTotalPrice => TotalPrice.FormatMoney();
    public string FormatServiceFee => ServiceFee.FormatMoney();

    [JsonIgnore]
    public PricingResponse? Pricing => Pricings.Where(_ => _.StartTime <= TotalDays && _.EndTime >= TotalDays).FirstOrDefault() ?? Pricings.FirstOrDefault();

    [JsonIgnore]
    public ICollection<PricingResponse> Pricings { get; set; } = new HashSet<PricingResponse>();
}
