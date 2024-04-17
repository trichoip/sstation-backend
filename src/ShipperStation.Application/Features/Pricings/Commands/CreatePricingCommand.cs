using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Pricings.Commands;
public sealed record CreatePricingCommand : IRequest<MessageResponse>
{
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public double PricePerUnit { get; set; }
    public double UnitDuration { get; set; }

    [JsonIgnore]
    public int StationId { get; set; }
}
