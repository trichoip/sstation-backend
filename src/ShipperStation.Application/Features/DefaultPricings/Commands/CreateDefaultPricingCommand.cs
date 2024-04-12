using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.DefaultPricings.Commands;
public sealed record CreateDefaultPricingCommand : IRequest<MessageResponse>
{
    public int FromDate { get; set; }
    public int ToDate { get; set; }
    public double Price { get; set; }
}
