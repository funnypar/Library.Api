using System.Text.Json;

namespace Business.Helpers
{
    public class JsonDateTimeConversion : System.Text.Json.Serialization.JsonConverter<DateTime?>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            DateTime.TryParse(reader.GetString(), out var date) ? date : null;

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value?.ToString(Format));
    }
}
