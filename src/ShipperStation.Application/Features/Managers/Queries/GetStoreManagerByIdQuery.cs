using MediatR;
using ShipperStation.Application.Features.Users.Models;

namespace ShipperStation.Application.Features.Managers.Queries;
public sealed record GetStoreManagerByIdQuery(Guid Id) : IRequest<UserResponse>;
