using MediatR;
using ShipperStation.Application.Features.Roles.Models;

namespace ShipperStation.Application.Features.Roles.Queries;
public sealed record GetRolesQuery : IRequest<IList<RoleResponse>>;