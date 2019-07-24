namespace FrontlineMaidBot.Models
{
    public class Extension
    {
        public Skill Skill { get; set; }
        public BuffFull BuffFull { get; set; }

        public Extension()
        {
            Skill = new Skill();
            BuffFull = new BuffFull();
        }
    }
}
