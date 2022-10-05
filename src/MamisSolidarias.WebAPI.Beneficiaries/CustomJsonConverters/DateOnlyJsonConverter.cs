using System.Globalization;
using System.Text.Json;

namespace MamisSolidarias.WebAPI.Beneficiaries.CustomJsonConverters;

internal class DateOnlyJsonConverter : System.Text.Json.Serialization.JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is { } str)
        {
            return DateOnly.ParseExact(str,Format,CultureInfo.CurrentCulture);
        }

        return new DateOnly();
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format,CultureInfo.InvariantCulture));
    }
}