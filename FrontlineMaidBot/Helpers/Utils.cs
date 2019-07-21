using FrontlineMaidBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontlineMaidBot.Helpers
{
    public static class Utils
    {
        private static readonly char[] _integers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

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
            if (!string.IsNullOrEmpty(production.Time))
            {
                header += $"Production time: <b>{DeNormalizeTime(production.Time)}</b>";
            }

            string aliases = string.Empty;
            if (production.Alias != null && production.Alias.Any())
            {
                aliases = $"<b>Also known as: </b>{string.Join(", ", production.Alias)}{Environment.NewLine}";
            }

            string advantages = string.Empty;
            if (!string.IsNullOrEmpty(production.Advantages))
            {
                advantages = $"<b>Advantages:</b>{Environment.NewLine}{production.Advantages}{Environment.NewLine}";
            }

            string disadvantages = string.Empty;
            if(!string.IsNullOrEmpty(production.Disadvantages))
            {
                disadvantages = $"<b>Disadvantages:</b>{Environment.NewLine}{production.Disadvantages}{Environment.NewLine}";
            }

            string summary = string.Empty;
            if (!string.IsNullOrEmpty(production.Summary))
            {
                summary = $"<b>Summary:</b>{Environment.NewLine}{production.Summary}{Environment.NewLine}";
            }

            return $"{header}{aliases}{advantages}{disadvantages}{summary}";
        }

        internal static string CreateResponseSuggestion(IEnumerable<ProductionResult> sug, string defaultResponse, string suggestion)
        {
            if (sug == null || !sug.Any())
                return defaultResponse;

            return suggestion + $"{Environment.NewLine}{string.Join(", ", sug.OrderBy(x=>x.Name).Select(x => $"<b>[{x.Category}]</b>{x.Name}" ))}.";
        }

        public static string NormalizeTime(string time)
        {
            var result = new StringBuilder();
            int i = 0;

            while (result.Length < 4)
            {
                if (i >= time.Length)
                {
                    result.Insert(0, 0);
                    i++;
                    continue;
                }

                if (!_integers.Contains(time[i]))
                {
                    i++;
                    continue;
                }

                result.Append(time[i]);
                i++;
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

    }
}
