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
    public class DollCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "doll";
        private const string _dataPath = "Dolls.json";
        private const string _default = "I'm sorry. I can't find anything.";
        private const string _empty = "Are you tired? I think you need some rest.";

        private readonly IEnumerable<ProductionResult> _dolls;

        public DollCommand(IStorage storage) : base(name: _commandName)
        {
            _dolls = storage.Load<List<ProductionResult>>(_dataPath).Where(x => !string.IsNullOrEmpty(x.Time));
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if(update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = ParseInput(update);

            string message;
            if (string.IsNullOrEmpty(input.ArgsInput))
            {
                message = _empty;
            }
            else
            {
                var dolls = GetDolls(input.ArgsInput);
                message = Utils.CreateResponse(dolls, _default);
            }

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                message,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }


        private IEnumerable<ProductionResult> GetDolls(string time)
        {
            if (string.IsNullOrEmpty(time))
                return new List<ProductionResult>();

            var normalTime = Utils.NormalizeTime(time);
            return _dolls.Where(x => x.Time == normalTime);
        }
    }
}