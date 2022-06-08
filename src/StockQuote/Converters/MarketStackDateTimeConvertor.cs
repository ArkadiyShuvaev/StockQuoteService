using System.Text.Json;
using System.Text.Json.Serialization;

/// Provides members to deserialize unsupported by System.Text.Json datetime offset: +0000
/// System.Text.Json by default  supports a strict date-time format only.
public class MarketStackDateTimeConvertor : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
    }
}
