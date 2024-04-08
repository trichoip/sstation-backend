using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed record DeletePackageCommand(Guid Id) : IRequest<MessageResponse>;