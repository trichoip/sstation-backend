namespace ShipperStation.Application.Extensions;
public static class PackageExtensions
{
    public static double CalculateServiceFee(double volume, int totalDays, double pricing)
    {
        if (totalDays == 1)
        {
            return 0;
        }

        var serviceFee = (volume * pricing * totalDays) / 1000;

        if (serviceFee < 1000)
        {
            return 1000;
        }

        return serviceFee;
    }
}
