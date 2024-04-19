namespace ShipperStation.Application.Extensions;
public static class PackageExtensions
{
    public static double CalculateServiceFee(double volume, double totalHours, double pricePerUnit, double unitDuration)
    {
        var pricing = (pricePerUnit * totalHours) / unitDuration;

        var serviceFee = (volume * pricing) / 1000;

        //if (serviceFee < 1000)
        //{
        //    return 1000;
        //}

        return serviceFee;
    }
}
