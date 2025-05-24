using semanticKernelSample1.Plugins.DTOs.BaseCharacter;

namespace semanticKernelSample1.Plugins.DTOs.ExtendedCharacter
{
    // Base class for extended character attributes
    public abstract class ExtendedClassAttributes : ClassAttributes
    {
        public Weapon Weapon { get; set; } = new();
        public Weapon AlternateWeapon { get; set; } = new();
        public string ArmorType { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
    }
}
