using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed record DeleteShelfCommand(int Id) : IRequest<MessageResponse>;