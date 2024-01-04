using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStoreManager;
public sealed record CreateStoreManagerCommand : IRequest<MessageResponse>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? FullName { get; set; }

}
