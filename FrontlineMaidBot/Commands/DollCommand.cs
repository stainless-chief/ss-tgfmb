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

        private readonly List<ProductionResult> _dolls;

        public DollCommand(IStorage storage) : base(name: _commandName)
        {
            _dolls = storage.Load<List<ProductionResult>>(_dataPath);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            var input = ParseInput(update);
            
            var dolls = GetDolls(input.ArgsInput);
            var message = Utils.CreateResponse(dolls, _default);

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
            
            return _dolls.Where(x => x.Time == Utils.NormalizeTime(time));
        }
    }
}