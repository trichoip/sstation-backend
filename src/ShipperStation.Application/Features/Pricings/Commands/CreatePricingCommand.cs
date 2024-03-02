using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Pricings.Commands;
public sealed record CreatePricingCommand : IRequest<MessageResponse>
{
    public int FromDate { get; set; }
    public int ToDate { get; set; }
    public double Price { get; set; }

    [JsonIgnore]
    public int StationId { get; set; }
}
