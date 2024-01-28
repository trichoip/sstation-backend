using MediatR;
using ShipperStation.Application.Features.Auth.Models;

namespace ShipperStation.Application.Features.Auth.Commands;
public sealed record RefreshTokenRequest(string RefreshToken) : IRequest<AccessTokenResponse>;

