namespace semanticKernelSample1.Plugins.DTOs.BaseCharacter
{
    // Fighter class attributes
    public class FighterClassAttributes : ClassAttributes
    {
        public int WeaponMastery { get; set; }
        public int ArmorRating { get; set; }
        public int BattleEndurance { get; set; }
        public string BattleCry { get; set; } = string.Empty;
    }
}
