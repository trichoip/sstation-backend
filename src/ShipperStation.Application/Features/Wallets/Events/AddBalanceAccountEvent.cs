using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.Wallets.Events;
internal sealed record AddBalanceAccountEvent : BaseEvent
{
    public double Balance { get; set; }
}
