using System.Text.Json.Serialization;

namespace semanticKernelSample1.Plugins.DTOs.ExtendedCharacter
{
    public class ExtendedCharacter
    {
        public string Name { get; set; } = string.Empty;
        public int Strength { get; set; }
        public int Resilience { get; set; }
        public int Wounds { get; set; }
        
        [JsonConverter(typeof(semanticKernelSample1.Plugins.DTOs.Converters.ExtendedClassAttributesConverter))]
        public ExtendedClassAttributes ClassAttributes { get; set; } = new ExtendedFighterClassAttributes();
    }
}
