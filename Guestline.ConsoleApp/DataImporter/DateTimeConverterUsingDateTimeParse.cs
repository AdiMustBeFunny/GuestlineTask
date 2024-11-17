using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Guestline.ConsoleApp.DataImporter
{
    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        private readonly StringToDateTimeParser _stringToDateTimeParser = new StringToDateTimeParser();

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            var dateString = reader.GetString() ?? string.Empty;
            return _stringToDateTimeParser.Parse(dateString);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
