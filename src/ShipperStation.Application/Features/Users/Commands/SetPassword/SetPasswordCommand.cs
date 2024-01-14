using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Users.Commands.SetPassword;
public sealed record SetPasswordCommand : IRequest<MessageResponse>
{
    public string NewPassword { get; set; } = default!;
}
