using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Users.Commands;
public sealed record CreateUserCommand : IRequest<MessageResponse>
{
    public string PhoneNumber { get; set; } = default!;
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
}
