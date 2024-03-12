using MediatR;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Stations.Commands;
public sealed record UpdateStationsByStoreManagerCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public ICollection<UpdateStationImageRequest> StationImages { get; set; } = new HashSet<UpdateStationImageRequest>();
}
