using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Users.Commands.ResetPassword;

public sealed record ResetPasswordRequest : IRequest<MessageResponse>
{
    public string NewPassword { get; init; } = default!;
}
