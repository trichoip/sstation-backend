using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record PackageStatusHistoryResponse : BaseAuditableEntityResponse<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public PackageStatus Status { get; set; }

}
