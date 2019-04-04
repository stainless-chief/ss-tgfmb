using FrontlineMaidBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontlineMaidBot.Helpers
{
    public static class Utils
    {
        public static string CreateResponse(IEnumerable<ProductionResult> collection, string defaultResponse)
        {
            if (collection == null || !collection.Any())
                return defaultResponse;

            var time = collection.First().DisplayTime;
            var response = string.Join
                (
                    $"{Environment.NewLine}",
                    collection.Select(x => $"{x.Stars} [{x.Category}]  {x.Name}")
                );

            return $"[{time}]{Environment.NewLine}{response}";
        }

        public static string NormalizeTime(string time)
        {
            var fixedTime = time.Replace(":", "");

            if(fixedTime.Length > 4)
            {
                fixedTime.Remove(4, fixedTime.Length - 4);
            }

            if (fixedTime.Length < 4)
            {
                var placeholder = Enumerable.Repeat("0", 4 - fixedTime.Length);

                fixedTime = string.Concat(placeholder) + fixedTime;
            }

            return fixedTime;
        }
    }
}
