using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Auth.Commands.ResetPassword;

public sealed record ResetPasswordRequest : IRequest<MessageResponse>
{
    public string NewPassword { get; init; } = default!;
}
