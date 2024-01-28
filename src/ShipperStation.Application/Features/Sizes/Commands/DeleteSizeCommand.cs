using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Sizes.Commands;
public sealed record DeleteSizeCommand(int Id) : IRequest<MessageResponse>;