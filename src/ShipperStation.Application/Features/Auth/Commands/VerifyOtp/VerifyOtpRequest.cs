using MediatR;
using ShipperStation.Application.Contracts.Auth;
using ShipperStation.Shared.Converters;

namespace ShipperStation.Application.Features.Auth.Commands.VerifyOtp;

public sealed record VerifyOtpRequest : IRequest<AccessTokenResponse>
{
    [NormalizePhone]
    public string PhoneNumber { get; init; } = default!;

    [TrimString(true)]
    public string Otp { get; init; } = default!;
}
