using Microsoft.Extensions.Configuration;

namespace ShipperStation.Shared.Extensions;

public static class ConfigurationExtensions
{
    public static T GetValueOrDefault<T>(this IConfiguration configuration, string key, T defaultValue)
    {
        try
        {
            var value = configuration.GetValue<T>(key);
            return value ?? defaultValue;
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }
}