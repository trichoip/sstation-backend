using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Utils;

public class TrimStringConverter : JsonConverter<string>
{
    private readonly bool _nullIfEmpty = false;

    public TrimStringConverter(bool nullIfEmpty) => _nullIfEmpty = nullIfEmpty;

    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        var trimmedValue = value?.Trim();
        if (_nullIfEmpty && string.IsNullOrWhiteSpace(trimmedValue))
        {
            return null;
        }
        return trimmedValue;
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}