using System.Text.Json;
using System.Text.Json.Serialization;
using semanticKernelSample1.Plugins.DTOs.BaseCharacter;

namespace semanticKernelSample1.Plugins.DTOs.Converters
{
    // Custom JSON converter for ClassAttributes polymorphism
    public class ClassAttributesConverter : JsonConverter<ClassAttributes>
    {
        public override ClassAttributes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("classType", out JsonElement classTypeElement))
            {
                string classType = classTypeElement.GetString() ?? "";
                
                return classType.ToLower() switch
                {
                    "fighter" => JsonSerializer.Deserialize<FighterClassAttributes>(root.GetRawText(), options)!,
                    "rogue" => JsonSerializer.Deserialize<RogueClassAttributes>(root.GetRawText(), options)!,
                    "mage" => JsonSerializer.Deserialize<MageClassAttributes>(root.GetRawText(), options)!,
                    _ => throw new JsonException($"Unknown class type: {classType}")
                };
            }
            
            throw new JsonException("Missing classType property");
        }

        public override void Write(Utf8JsonWriter writer, ClassAttributes value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
