using MediatR;
using ShipperStation.Application.Contracts;
using ShipperStation.Shared.Converters;

namespace ShipperStation.Application.Features.Auth.Commands.SendOtp;
public sealed record SendOtpRequest : IRequest<MessageResponse>
{
    [NormalizePhone]
    public string PhoneNumber { get; init; } = default!;
}
