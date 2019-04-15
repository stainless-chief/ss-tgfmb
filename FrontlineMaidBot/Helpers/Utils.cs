using FrontlineMaidBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontlineMaidBot.Helpers
{
    public static class Utils
    {
        private static char[] _integers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static string CreateResponse(IEnumerable<ProductionResult> collection, string defaultResponse)
        {
            if (collection == null || !collection.Any())
                return defaultResponse;

            var time = DeNormalizeTime(collection.First().Time);
            var response = string.Join
                (
                    $"{Environment.NewLine}",
                    collection.Select(x => $"{x.Stars} [{x.Category}]  {x.Name}")
                );

            return $"[{time}]{Environment.NewLine}{response}";
        }

        public static string CreateResponse(ProductionResult production, string defaultResponse)
        {
            if (production == null)
                return defaultResponse;

            string header = string.Empty;

            if (!string.IsNullOrEmpty(production.Stars))
            {
                header = $"[{production.Stars}] ";
            }
            if (!string.IsNullOrEmpty(production.Category))
            {
                header += $"<b>[{production.Category}]</b> ";
            }
            if (!string.IsNullOrEmpty(production.Name))
            {
                header += $"{production.Name} ";
            }
            if (!string.IsNullOrEmpty(header))
            {
                header += Environment.NewLine;
            }

            var aliases = production.Alias == null ? string.Empty : $"<b>Also known as: </b>{string.Join(", ", production.Alias)}{Environment.NewLine}";
            var summary = production.Summary == null ? string.Empty : $"{Environment.NewLine}{production.Summary}{Environment.NewLine}";

            return $"{header}{aliases}{summary}";
        }
        
        public static string NormalizeTime(string time)
        {
            var result = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                if (i >= time.Length)
                {
                    result.Insert(0, 0);
                    continue;
                }

                if (!_integers.Contains(time[i]))
                    continue;

                result.Append(time[i]);
            }

            return result.ToString();
        }

        public static string DeNormalizeTime(string time)
        {
            var result = new StringBuilder();
            int counter = 0;
            foreach (var c in time)
            {
                result.Append(c);
                counter++;

                if (counter == 2)
                {
                    counter = 0;
                    result.Append(':');
                }
            }

            result.Append("00");

            return result.ToString();
        }

        //public static void DoDirtyWork()
        //{
        //    var stor = new DAL.Storage();
        //    var ss = stor.Load<List<ProductionResult>>(@"Equipment.json");

        //    ss = ss.OrderBy(x => x.Category).ThenBy(x => x.Stars).ToList();

        //    var f = Newtonsoft.Json.JsonConvert.SerializeObject(ss);
        //}
    }
}
