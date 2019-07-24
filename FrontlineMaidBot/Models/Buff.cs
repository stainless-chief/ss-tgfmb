using System.Collections.Generic;

namespace FrontlineMaidBot.Models
{
    public class BuffFull
    {
        public string Target { get; set; }
        public string Pattern { get; set; }
        public Dictionary<string, int> Effects { get; set; }

        public BuffFull()
        {
            Effects = new Dictionary<string, int>();
        }
    }
}
