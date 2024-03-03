using MediatR;
using ShipperStation.Application.Features.Users.Models;

namespace ShipperStation.Application.Features.Users.Queries;
public sealed record GetUserByPhoneQuery(string number) : IRequest<UserResponse>;
