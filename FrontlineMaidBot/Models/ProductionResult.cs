using System.Collections.Generic;

namespace FrontlineMaidBot.Models
{
    public class ProductionResult
    {
        public string Advantages { get; set; }
        public List<string> Alias { get; set; }
        public string Category { get; set; }
        public string Disadvantages { get; set; }
        
        public string Name { get; set; }
        public string Stars { get; set; }
        public string Summary { get; set; }

        /// <summary>Production time in format of string of 4 digits: hhmm</summary>
        public string Time { get; set; }

        /// <summary> String with all possible names to search by</summary>
        public string Lookup { get; set; }

        /// <summary> Additional information</summary>
        public Extension Extension { get; set; }

        public ProductionResult()
        {
            Extension = new Extension();
        }
    }
}