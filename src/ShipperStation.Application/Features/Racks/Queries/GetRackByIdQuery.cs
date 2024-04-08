using MediatR;
using ShipperStation.Application.Features.Racks.Models;

namespace ShipperStation.Application.Features.Racks.Queries;
public sealed record GetRackByIdQuery(int Id) : IRequest<RackResponse>;