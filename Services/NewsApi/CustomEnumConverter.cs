using Newtonsoft.Json;

namespace DroneNews.Services.NewsApi;
public class CustomEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (Enum.TryParse<T>(reader.Value.ToString(), true, out var value))
        {
            return value;
        }

        return default; // or handle the error appropriately
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}
