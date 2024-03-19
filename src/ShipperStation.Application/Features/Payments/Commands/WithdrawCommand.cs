using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Payments.Commands;
public sealed record WithdrawCommand : IRequest<MessageResponse>
{
    public double Amount { get; set; }
}
