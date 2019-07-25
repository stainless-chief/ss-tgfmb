namespace FrontlineMaidBot.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }        
        public int? Cooldown { get; set; }        
        public int? InitCooldown { get; set; }        
    }
}