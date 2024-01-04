using MediatR;
using ShipperStation.Application.Contracts.Auth;

namespace ShipperStation.Application.Features.Auth.Commands.RefreshToken;
public sealed record RefreshTokenRequest(string RefreshToken) : IRequest<AccessTokenResponse>;

