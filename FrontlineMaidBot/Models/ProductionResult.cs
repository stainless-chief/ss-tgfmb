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

        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public string Summary { get; set; }

        public string Lookup { get; set; }

        public Extension Extension { get; set; }

        public ProductionResult()
        {
            Extension = new Extension();
        }
    }

}
