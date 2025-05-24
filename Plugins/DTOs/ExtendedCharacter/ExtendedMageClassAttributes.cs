namespace semanticKernelSample1.Plugins.DTOs.ExtendedCharacter
{
    // Extended Mage class attributes
    public class ExtendedMageClassAttributes : ExtendedClassAttributes
    {
        public string MagicType { get; set; } = string.Empty;
        public int Spellcasting { get; set; }
        public int ManaPool { get; set; }
        public int Memory { get; set; }
        public int ArcaneResistance { get; set; }
        public List<Spell> Spells { get; set; } = new();
        public string Companion { get; set; } = string.Empty;
    }
}
