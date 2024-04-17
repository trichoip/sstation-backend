using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.DefaultPricings.Commands;
public sealed record UpdateDefaultPricingCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public double PricePerUnit { get; set; }
    public double UnitDuration { get; set; }
}
