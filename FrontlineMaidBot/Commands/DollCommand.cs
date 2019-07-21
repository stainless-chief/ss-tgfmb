using FrontlineMaidBot.Helpers;
using FrontlineMaidBot.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class DollCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "doll";
        private const string _default = "I'm sorry. I can't find anything.";
        private IStorage _storage;

        public DollCommand(IStorage storage) : base(name: _commandName)
        {
            _storage = storage;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = ParseInput(update);
            var dolls = _storage.GetDollsByName(input.ArgsInput);
            var message = Utils.CreateResponse(dolls, _default);

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                message,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }

    }
}