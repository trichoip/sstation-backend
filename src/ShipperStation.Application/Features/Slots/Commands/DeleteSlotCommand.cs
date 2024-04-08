using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Slots.Commands;
public sealed record DeleteSlotCommand(int Id) : IRequest<MessageResponse>;