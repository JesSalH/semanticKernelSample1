namespace semanticKernelSample1.Plugins.DTOs.ExtendedCharacter
{
    // Extended Rogue class attributes
    public class ExtendedRogueClassAttributes : ExtendedClassAttributes
    {
        public int Stealth { get; set; }
        public int Agility { get; set; }
        public int LockPicking { get; set; }
        public string RogueSchool { get; set; } = string.Empty;
        public string MasterSkill { get; set; } = string.Empty;
    }
}
