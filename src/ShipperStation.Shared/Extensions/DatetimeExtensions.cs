using ShipperStation.Shared.Constants;

namespace ShipperStation.Shared.Extensions;

public static class DatetimeExtensions
{
    public static string ToString(this DateTimeOffset dateTime, string timeZoneId, string format = DateTimeConstants.DateTimeFormat)
    {
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var convertedTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
        return convertedTime.ToString(format);
    }
}