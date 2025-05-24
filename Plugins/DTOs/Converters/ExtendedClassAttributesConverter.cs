using System.Text.Json;
using System.Text.Json.Serialization;
using semanticKernelSample1.Plugins.DTOs.ExtendedCharacter;

namespace semanticKernelSample1.Plugins.DTOs.Converters
{
    // Custom JSON converter for ExtendedClassAttributes polymorphism
    public class ExtendedClassAttributesConverter : JsonConverter<ExtendedClassAttributes>
    {
        public override ExtendedClassAttributes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("classType", out JsonElement classTypeElement))
            {
                string classType = classTypeElement.GetString() ?? "";
                
                return classType.ToLower() switch
                {
                    "fighter" => JsonSerializer.Deserialize<ExtendedFighterClassAttributes>(root.GetRawText(), options)!,
                    "rogue" => JsonSerializer.Deserialize<ExtendedRogueClassAttributes>(root.GetRawText(), options)!,
                    "mage" => JsonSerializer.Deserialize<ExtendedMageClassAttributes>(root.GetRawText(), options)!,
                    _ => throw new JsonException($"Unknown extended class type: {classType}")
                };
            }
            
            throw new JsonException("Missing classType property in extended character");
        }

        public override void Write(Utf8JsonWriter writer, ExtendedClassAttributes value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
