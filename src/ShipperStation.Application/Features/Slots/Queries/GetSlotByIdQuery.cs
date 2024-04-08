using MediatR;
using ShipperStation.Application.Features.Slots.Models;

namespace ShipperStation.Application.Features.Slots.Queries;
public sealed record GetSlotByIdQuery(int Id) : IRequest<SlotResponse>;