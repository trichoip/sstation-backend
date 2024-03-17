namespace ShipperStation.Application.Extensions;
public static class PackageExtensions
{
    public static double CalculateTotalPrice(double priceCod, double volume, int totalDays, double pricing)
    {
        return priceCod + ((volume + pricing) * totalDays);
    }
}
