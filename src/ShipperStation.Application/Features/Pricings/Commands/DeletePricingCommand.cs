using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Pricings.Commands;
public sealed record DeletePricingCommand(int Id) : IRequest<MessageResponse>;
