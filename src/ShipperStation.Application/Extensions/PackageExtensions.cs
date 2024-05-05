namespace ShipperStation.Application.Extensions;
public static class PackageExtensions
{
    public static double CalculateServiceFee(double volume, double totalDays, double price)
    {
        var serviceFee = (volume * price * totalDays) / 1000;

        return serviceFee;
    }
}
