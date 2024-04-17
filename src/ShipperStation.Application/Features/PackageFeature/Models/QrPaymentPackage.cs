using ShipperStation.Application.Extensions;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Extensions;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record QrPaymentPackage
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double PriceCod { get; set; }
    public bool IsCod { get; set; }
    public PackageStatus Status { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    public Guid SenderId { get; set; }
    //public UserResponse Sender { get; set; } = default!;

    public Guid ReceiverId { get; set; }
    //public UserResponse Receiver { get; set; } = default!;

    public string Location { get; set; } = default!;

    public double TotalHours { get; set; }
    public double TotalPrice => PriceCod + ServiceFee;

    public double ServiceFee => Pricing is null ? 0 : PackageExtensions.CalculateServiceFee(Volume, TotalHours, Pricing.PricePerUnit, Pricing.UnitDuration);

    public string FormatTotalPrice => TotalPrice.FormatMoney();
    public string FormatServiceFee => ServiceFee.FormatMoney();

    [JsonIgnore]
    public PricingResponse? Pricing => Pricings.Where(_ => _.StartTime <= TotalHours && _.EndTime >= TotalHours).FirstOrDefault();

    [JsonIgnore]
    public ICollection<PricingResponse> Pricings { get; set; } = new HashSet<PricingResponse>();

    public ICollection<PackageImageResponse> PackageImages { get; set; } = new HashSet<PackageImageResponse>();

    //public ICollection<PackageStatusHistoryResponse> PackageStatusHistories { get; set; } = new HashSet<PackageStatusHistoryResponse>();
}
