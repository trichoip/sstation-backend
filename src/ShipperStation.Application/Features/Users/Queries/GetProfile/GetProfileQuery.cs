using MediatR;
using ShipperStation.Application.Contracts.Users;

namespace ShipperStation.Application.Features.Users.Queries.GetProfile;
public sealed record GetProfileQuery : IRequest<UserResponse>;
