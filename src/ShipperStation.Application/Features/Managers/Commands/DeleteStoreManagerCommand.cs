using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Managers.Commands;
public sealed record DeleteStoreManagerCommand(Guid Id) : IRequest<MessageResponse>;