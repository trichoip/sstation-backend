using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Users.Commands;
public sealed record UpdateUserForAdminCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }

    public bool IsActive { get; set; }
}
