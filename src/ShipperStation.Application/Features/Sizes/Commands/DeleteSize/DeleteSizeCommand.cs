using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Sizes.Commands.DeleteSize;
public sealed record DeleteSizeCommand(int Id) : IRequest<MessageResponse>;