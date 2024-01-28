using MediatR;
using ShipperStation.Application.Features.Auth.Models;
using System.ComponentModel;

namespace ShipperStation.Application.Features.Auth.Commands;
public sealed record LoginRequest : IRequest<AccessTokenResponse>
{
    [DefaultValue("admin")]
    public string Username { get; init; } = default!;

    [DefaultValue("admin")]
    public string Password { get; init; } = default!;

}
