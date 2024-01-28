using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Users.Commands;
public sealed record UpdateProfileCommand : IRequest<MessageResponse>
{
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
}
