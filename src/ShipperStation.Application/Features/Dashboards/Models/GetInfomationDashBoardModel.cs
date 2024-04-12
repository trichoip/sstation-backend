namespace ShipperStation.Application.Features.Dashboards.Models;
public sealed record GetInfomationDashBoardModel
{
    public DashBoardType DashBoardType { get; init; }
    public double Value { get; init; }

    public double Percent { get; init; }

}

public enum DashBoardType
{
    TodaySales,
    TodayUsers,
    NewClient,
    NewOrders,
}