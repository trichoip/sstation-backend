using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.DefaultPricings.Commands;
public sealed record CreateDefaultPricingCommand : IRequest<MessageResponse>
{
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public double PricePerUnit { get; set; }
    public double UnitDuration { get; set; }
}
