using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Racks.Commands;
public sealed record DeleteRackCommand(int Id) : IRequest<MessageResponse>;