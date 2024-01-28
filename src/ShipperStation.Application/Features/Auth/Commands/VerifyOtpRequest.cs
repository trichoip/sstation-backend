using MediatR;
using ShipperStation.Application.Features.Auth.Models;
using ShipperStation.Shared.Converters;

namespace ShipperStation.Application.Features.Auth.Commands;

public sealed record VerifyOtpRequest : IRequest<AccessTokenResponse>
{
    [NormalizePhone]
    public string PhoneNumber { get; init; } = default!;

    [TrimString(true)]
    public string Otp { get; init; } = default!;
}
