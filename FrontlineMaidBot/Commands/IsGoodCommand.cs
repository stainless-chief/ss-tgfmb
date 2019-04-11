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
        private const string _dataPath = "Dolls.json";
        private const string _default = "I'm sorry. I don't know.";
        private const string _empty = "Is something wrong?";

        private readonly List<ProductionResult> _dolls;

        public IsGoodCommand(IStorage storage) : base(name: _commandName)
        {
            _dolls = storage.Load<List<ProductionResult>>(_dataPath);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            string msg;
            var input = base.ParseInput(update);

            if (string.IsNullOrEmpty(input.ArgsInput))
            {
                msg = _empty;
            }
            else
            {
                msg = Utils.CreateResponse(GetDoll(input.ArgsInput), _default);
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


        private ProductionResult GetDoll(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var firstRun = _dolls.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));

            if (firstRun != null)
                return firstRun;

            var secondRun = _dolls.Where(x => x.Alias != null).FirstOrDefault(x => x.Alias.Contains(name.ToLower()));

            if (secondRun != null)
                return secondRun;

            return null;
        }

    }
}
