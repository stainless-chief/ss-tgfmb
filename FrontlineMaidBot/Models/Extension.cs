namespace FrontlineMaidBot.Models
{
    public class Extension
    {
        public Skill Skill { get; set; }
        public Buff Buff { get; set; }

        public Extension()
        {
            Skill = new Skill();
            Buff = new Buff();
        }
    }
}
