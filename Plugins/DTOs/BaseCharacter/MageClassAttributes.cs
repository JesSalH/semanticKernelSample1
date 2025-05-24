namespace semanticKernelSample1.Plugins.DTOs.BaseCharacter
{
    // Mage class attributes
    public class MageClassAttributes : ClassAttributes
    {
        public string MagicType { get; set; } = string.Empty;
        public int Spellcasting { get; set; }
        public int ManaPool { get; set; }
        public int Memory { get; set; }
        public int ArcaneResistance { get; set; }
    }
}
