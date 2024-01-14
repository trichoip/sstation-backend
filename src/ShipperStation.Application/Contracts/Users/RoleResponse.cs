namespace ShipperStation.Application.Contracts.Users;
public sealed record RoleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
