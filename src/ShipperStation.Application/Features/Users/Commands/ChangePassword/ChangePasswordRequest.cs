using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Users.Commands.ChangePassword;
public sealed record ChangePasswordRequest : IRequest<MessageResponse>
{
    public string OldPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
