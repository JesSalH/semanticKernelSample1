using System.Text.Json.Serialization;

namespace semanticKernelSample1.Plugins.DTOs.BaseCharacter
{
    public class BaseCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Strength { get; set; }
        public int Resilience { get; set; }
        
        [JsonConverter(typeof(semanticKernelSample1.Plugins.DTOs.Converters.ClassAttributesConverter))]
        public ClassAttributes ClassAttributes { get; set; } = new FighterClassAttributes();
    }
}
