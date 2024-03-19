namespace ShipperStation.Application.Extensions;
public static class PackageExtensions
{
    public static double CalculateServiceFee(double volume, int totalDays, double pricing)
    {
        return (volume * pricing * totalDays) / 1000;
    }
}
