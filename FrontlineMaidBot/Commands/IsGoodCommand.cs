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

        private readonly IDefaultMessages _defaultMessages;
        private readonly IResponseGenerator _generator;
        private readonly IStorage _storage;

        public IsGoodCommand(IStorage storage, IResponseGenerator generator, IDefaultMessages defaultMessages) : base(name: _commandName)
        {
            _storage = storage;
            _generator = generator;
            _defaultMessages = defaultMessages;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = base.ParseInput(update);

            if (string.IsNullOrEmpty(input?.ArgsInput))
            {
                await Send(update.Message.Chat.Id, _defaultMessages.WrongParams, update.Message.MessageId);
                return UpdateHandlingResult.Handled;
            }

            var dolls = _storage.GetByName(input.ArgsInput);
            var count = dolls.Count();

            string msg;
            if (count <= 1)
            {
                msg = _generator.CreateSummaryMessage(dolls.FirstOrDefault(), _defaultMessages.DontKnow);
            }
            else
            {
                msg = _generator.CreateSuggestionMessage(dolls, _defaultMessages.DontKnow, _defaultMessages.Suggestion);
            }

            await Send(update.Message.Chat.Id, msg, update.Message.MessageId);
            return UpdateHandlingResult.Handled;
        }

        private Task<Message> Send(long chatId, string message, int messageId)
        {
            return Bot.Client.SendTextMessageAsync
            (
                chatId,
                message,
                Telegram.Bot.Types.Enums.ParseMode.Html,
                replyToMessageId: messageId
            );
        }
    }
}