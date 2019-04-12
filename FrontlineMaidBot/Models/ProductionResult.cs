using System.Collections.Generic;

namespace FrontlineMaidBot.Models
{
    public class ProductionResult
    {
        public string Name { get; set; }
        public List<string> Alias { get; set; }
        public string Stars { get; set; }
        public string Category { get; set; }
        public string Time { get; set; }
        public string Summary { get; set; }
    }
}
