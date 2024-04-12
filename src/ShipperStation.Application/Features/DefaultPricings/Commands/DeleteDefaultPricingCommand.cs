using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.DefaultPricings.Commands;
public sealed record DeleteDefaultPricingCommand(int Id) : IRequest<MessageResponse>;