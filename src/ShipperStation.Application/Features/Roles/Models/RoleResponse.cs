namespace ShipperStation.Application.Features.Roles.Models;
public sealed record RoleResponse
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
}
