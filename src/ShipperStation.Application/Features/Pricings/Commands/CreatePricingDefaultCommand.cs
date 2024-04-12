using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Pricings.Commands;
public sealed record CreatePricingDefaultCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int StationId { get; set; }
}
