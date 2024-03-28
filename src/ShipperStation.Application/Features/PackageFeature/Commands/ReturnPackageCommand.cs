using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed record ReturnPackageCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
