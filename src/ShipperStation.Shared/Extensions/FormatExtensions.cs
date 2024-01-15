using System.Globalization;

namespace ShipperStation.Shared.Extensions;
public static class FormatExtensions
{
    public const string kg = nameof(kg);
    public const string cm = nameof(cm);
    public const string cm3 = nameof(cm3);

    public static string FormatMoney(this double value)
    {
        return string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:C}", value);
    }

    public static string FormatValue(this double value, string unit)
    {
        return $"{value:N1} {unit}";
    }

    public static double Round(this double value, int digits)
    {
        return Math.Round(value, digits);
    }
}

