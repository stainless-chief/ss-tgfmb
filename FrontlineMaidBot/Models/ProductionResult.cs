using System.Collections.Generic;

namespace FrontlineMaidBot.Models
{
    public class ProductionResult
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<string> Alias { get; set; }
        public string Stars { get; set; }
        public string Category { get; set; }
        public string Time { get; set; }
        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public string Summary { get; set; }

        public string Lookup { get; set; }

        public Extension Extension { get; set; }
    }

    public class Extension
    {
        public Stats Stats { get; set; }
        public Skill Skill { get; set; }
        public string Buff { get; set; }
    }


    public class Stats
    {
        public int Damage { get; set; }
        public int Accuracity { get; set; }
        public int Evasion { get; set; }
        public int RateOfFire { get; set; }
        public int HP { get; set; }
    }

    public class Skill
    {
        public string Description { get; set; }
        public int InitialCD { get; set; }
        public int SkillCD { get; set; }
    }
}
