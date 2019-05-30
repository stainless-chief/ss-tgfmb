using FrontlineMaidBot.Helpers;
using FrontlineMaidBot.Interfaces;
using FrontlineMaidBot.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class IsGoodCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "isgood";
        private const string _default = "I'm sorry. I don't know.";
        private const string _empty = "I'm sorry. What?";
        private const string _suggestion = "I'm sorry... did you mean someone like:";

        private readonly IEnumerable<ProductionResult> _dolls;

        public IsGoodCommand(IStorage storage) : base(name: _commandName)
        {
            _dolls = storage.GetDolls();
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = base.ParseInput(update);
            string msg = _empty;

            if (!string.IsNullOrEmpty(input.ArgsInput))
            {
                var doll = Search(input.ArgsInput.ToLower());
                if (doll != null)
                {
                    msg = Utils.CreateResponse(doll, _default);
                }
                else
                {
                    var sug = SearchDeep(input.ArgsInput.ToLower());

                    if (sug != null && sug.Count() == 1)
                    {
                        msg = Utils.CreateResponse(sug.First(), _default);
                    }
                    else
                    {
                        msg = Utils.CreateResponseSuggestion(sug, _default, _suggestion);
                    }
                }
            }

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                msg,
                Telegram.Bot.Types.Enums.ParseMode.Html,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }


        private ProductionResult Search(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var firstRun = _dolls.FirstOrDefault(x => x.Name.ToLower().Equals(name));

            if (firstRun != null)
                return firstRun;

            var secondRun = _dolls.FirstOrDefault(x => x.Alias != null && x.Alias.Any(al => al.ToLower() == name));

            if (secondRun != null)
                return secondRun;

            return null;
        }

        private IEnumerable<ProductionResult> SearchDeep(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var result = _dolls.Where(x => x.Lookup.Contains(name));

            return result;
        }

    }
}
