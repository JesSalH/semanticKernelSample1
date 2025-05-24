namespace semanticKernelSample1.Plugins.DTOs.ExtendedCharacter
{
    public class Spell
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MagicType { get; set; } = string.Empty;
        public int Memory { get; set; }
        public int ManaCost { get; set; }
    }
}
