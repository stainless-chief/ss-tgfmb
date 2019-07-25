namespace FrontlineMaidBot.Models
{
    public class Extension
    {
        public BuffFull BuffFull { get; set; }
        public Skill Skill { get; set; }

        public Extension()
        {
            Skill = new Skill();
            BuffFull = new BuffFull();
        }
    }
}