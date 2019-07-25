using FrontlineMaidBot.Helpers;
using FrontlineMaidBot.Interfaces;
using FrontlineMaidBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontlineMaidBot.Generator
{
    public class ResponseGenerator : IResponseGenerator
    {
        public string CreateTimerMessage(IEnumerable<ProductionResult> collection, string defaultResponse)
        {
            if (collection == null || !collection.Any())
                return defaultResponse;

            var time = Utils.DeNormalizeTime(collection.First().Time);
            var response = string.Join
                (
                    $"{Environment.NewLine}",
                    collection.Select(x => $"{x.Stars} [{x.Category}]  {x.Name}")
                );

            return $"<b>[{time}]</b>{Environment.NewLine}{response}";
        }

        public string CreateSummaryMessage(ProductionResult production, string defaultResponse)
        {
            if (production == null)
                return defaultResponse;

            var result = new StringBuilder();

            if (!string.IsNullOrEmpty(production.Stars))
                result.Append($"<b>[{production.Stars}]</b>");

            if (!string.IsNullOrEmpty(production.Category))
                result.Append($"<b>[{production.Category}]</b>");

            if (!string.IsNullOrEmpty(production.Name))
                result.Append($"<b>[{production.Name}]</b>");

            if (production.Alias != null && production.Alias.Any())
                result.Append($"{Environment.NewLine}{alsoKnowAs}{string.Join(",", production.Alias)}");

            if (!string.IsNullOrEmpty(production.Advantages))
                result.Append($"{Environment.NewLine}{advantages}{Environment.NewLine}{production.Advantages}");

            if (!string.IsNullOrEmpty(production.Disadvantages))
                result.Append($"{Environment.NewLine}{disadvantages}{Environment.NewLine}{production.Disadvantages}");

            if (!string.IsNullOrEmpty(production.Summary))
                result.Append($"{Environment.NewLine}{summary}{Environment.NewLine}{production.Summary}");

            if (result.Length == 0)
                return defaultResponse;

            return result.ToString();
        }

        public string CreateInfoMessage(ProductionResult production, string defaultResponse)
        {
            if (production == null)
                return defaultResponse;

            var result = new StringBuilder();

            if (!string.IsNullOrEmpty(production.Stars))
                result.Append($"<b>[{production.Stars}]</b>");

            if (!string.IsNullOrEmpty(production.Category))
                result.Append($"<b>[{production.Category}]</b>");

            if (!string.IsNullOrEmpty(production.Name))
                result.Append($"<b>[{production.Name}]</b>");

            if (production.Alias!= null && production.Alias.Any())
                result.Append($"{Environment.NewLine}{alsoKnowAs}{string.Join(",", production.Alias)}");

            if(production.Extension?.Skill != null)
            {
                if (!string.IsNullOrEmpty(production.Extension.Skill.Name))
                {
                    result.Append($"{Environment.NewLine}{skill} {production.Extension.Skill.Name}");

                    if (production.Extension.Skill.InitCooldown.HasValue)
                        result.Append($"{initCd}{production.Extension.Skill.InitCooldown} seconds. ");

                    if (production.Extension.Skill.Cooldown.HasValue)
                        result.Append($"{Cd}{production.Extension.Skill.Cooldown} seconds.");

                    if (!string.IsNullOrEmpty(production.Extension?.Skill?.Description))
                        result.Append($"{Environment.NewLine}{production.Extension.Skill.Description}");
                }
            }
            if (production.Extension?.BuffFull != null)
            {
                if (!string.IsNullOrEmpty(production.Extension.BuffFull.Target))
                {
                    result.Append($"{Environment.NewLine}{buffs}<b>{production.Extension.BuffFull.Target}</b>");

                    if (production.Extension.BuffFull.Effects.Any())
                    {
                        result.Append($"{Environment.NewLine}{string.Join(", ", production.Extension.BuffFull.Effects.Select(x => $"{x.Key} {x.Value}%"))}");
                    }
                }
            }

            return result.ToString();
        }

        public string CreateSuggestionMessage(IEnumerable<ProductionResult> sug, string defaultResponse, string suggestion)
        {
            if (sug == null || !sug.Any())
                return defaultResponse;

            return suggestion + $"{Environment.NewLine}{string.Join(", ", sug.OrderBy(x => x.Name).Select(x => $"<b>[{x.Category}]</b> {x.Name}"))}.";
        }

        const string buffs = "<b>Buffs </b>";


        const string skill = "<b>Skill (10 lvl):</b>";


        const string alsoKnowAs = "<b>Also known as: </b>";
        const string advantages = "<b>Advantages: </b>";
        const string disadvantages = "<b>Disadvantages: </b>";
        const string summary = "<b>Summary: </b>";
        const string initCd = "<b>Initial cooldown: </b>";
        const string Cd = "<b>Cooldown: </b>";
    }
}
