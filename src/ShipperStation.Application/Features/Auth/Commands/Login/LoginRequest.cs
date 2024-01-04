using MediatR;
using ShipperStation.Application.Contracts.Auth;
using System.ComponentModel;

namespace ShipperStation.Application.Features.Auth.Commands.Login;
public sealed record LoginRequest : IRequest<AccessTokenResponse>
{
    [DefaultValue("admin")]
    public string Username { get; init; } = default!;

    [DefaultValue("admin")]
    public string Password { get; init; } = default!;

}
