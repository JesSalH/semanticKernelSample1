namespace semanticKernelSample1.Plugins.DTOs.ExtendedCharacter
{
    // Extended Fighter class attributes
    public class ExtendedFighterClassAttributes : ExtendedClassAttributes
    {
        public int WeaponMastery { get; set; }
        public int ArmorRating { get; set; }
        public int BattleEndurance { get; set; }
        public string BattleCry { get; set; } = string.Empty;
        public string SpecialMeleeAttack { get; set; } = string.Empty;
    }
}
