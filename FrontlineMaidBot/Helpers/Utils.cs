using FrontlineMaidBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static string CreateResponse(ProductionResult production, string defaultResponse)
        {
            if (production == null)
                return defaultResponse;

            string header = string.Empty;

            if(!string.IsNullOrEmpty(production.Stars))
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
            if(!string.IsNullOrEmpty(header))
            {
                header += Environment.NewLine;
            }

            var aliases = production.Alias == null? string.Empty : $"<b>Also known as: </b>{string.Join(", ", production.Alias)}{Environment.NewLine}";
            var summary = production.Summary == null ? string.Empty : $"{Environment.NewLine}{production.Summary}{Environment.NewLine}";

            return $"{header}{aliases}{summary}";
        }



        public static string NormalizeTime(string time)
        {
            var fixedTime = time.Replace(":", "");

            if(fixedTime.Length > 4)
            {
                fixedTime = fixedTime.Remove(4, fixedTime.Length - 4);
            }


            if (fixedTime.Length < 4)
            {
                var placeholder = Enumerable.Repeat("0", 4 - fixedTime.Length);

                fixedTime = string.Concat(placeholder) + fixedTime;
            }

            return fixedTime;
        }

        //private static void DoDirtyWork(string filename)
        //{
        //    var ss = new List<ProductionResult>();

        //    var str = File.ReadAllLines(filename);

        //    foreach (var item in str)
        //    {
        //        var spl = item.Split('—');

        //        ss.Add(new ProductionResult
        //        {
        //            Time = NormalizeTime(spl[0]),
        //            DisplayTime = spl[0],
        //            Category = "[Equipment]",
        //            Name = spl[1]
        //        });
        //    }
        //    var f = JsonConvert.SerializeObject(ss);
        //}
    }
}
