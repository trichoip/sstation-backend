using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed record UpdateLocationPackageCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public int CurrentSlotId { get; set; }

    public int NewSlotId { get; set; }

    public bool IsForce { get; set; }
}
