namespace ShipperStation.Application.Features.Dashboards.Models;
public sealed record UserCountByMonthResponse
{
    public int Month { get; init; }
    public int Year { get; init; }
    public int UserCount { get; init; }
}
