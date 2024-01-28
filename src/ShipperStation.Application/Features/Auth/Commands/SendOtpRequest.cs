using MediatR;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Converters;

namespace ShipperStation.Application.Features.Auth.Commands;
public sealed record SendOtpRequest : IRequest<MessageResponse>
{
    [NormalizePhone]
    public string PhoneNumber { get; init; } = default!;
}
