namespace FrontlineMaidBot.Models
{
    public class Buff
    {
        public string Target { get; set; }

        public int? DamageBonus { get; set; }
        public int? CritDamageBonus { get; set; }
        public int? AccuracyBonus { get; set; }
        public int? EvasionBonus { get; set; }
        public int? ArmorBonus { get; set; }
        public int? RateOfFireBonus { get; set; }
        public int? CriticalRateBonus { get; set; }
        public int? ReducesSkillCD { get; set; }

        public string Pattern { get; set; }
    }
}
