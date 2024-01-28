using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Users.Commands;

public sealed record ResetPasswordRequest : IRequest<MessageResponse>
{
    public string NewPassword { get; init; } = default!;
}
