using FrontlineMaidBot.Helpers;
using FrontlineMaidBot.Interfaces;
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
        private const string _empty = "You must specify girl name.";
        private const string _suggestion = "I'm sorry... did you mean someone like:";

        private readonly IStorage _storage;
        private readonly IResponseGenerator _generator;

        public IsGoodCommand(IStorage storage, IResponseGenerator generator) : base(name: _commandName)
        {
            _storage = storage;
            _generator = generator;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = base.ParseInput(update);
            string msg;

            if (string.IsNullOrEmpty(input.ArgsInput))
            {
                msg = _empty;
            }
            else
            {
                msg = _default;
                var dolls = _storage.GetDollsByName(input.ArgsInput);
                var count = dolls.Count();

                if (count == 1)
                {
                    msg = _generator.CreateSummaryMessage(dolls.First(), _default);
                }
                else if (count > 1)
                {
                    msg = _generator.CreateSuggestionMessage(dolls, _default, _suggestion);
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



    }
}
