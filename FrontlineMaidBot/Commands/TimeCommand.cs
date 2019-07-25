using FrontlineMaidBot.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class TimeCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "time";

        private readonly IDefaultMessages _defaultMessages;
        private readonly IResponseGenerator _generator;
        private readonly IStorage _storage;

        public TimeCommand(IStorage storage, IResponseGenerator generator, IDefaultMessages defaultMessages) : base(name: _commandName)
        {
            _storage = storage;
            _generator = generator;
            _defaultMessages = defaultMessages;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = ParseInput(update);
            if (string.IsNullOrEmpty(input?.ArgsInput))
            {
                await Send(update.Message.Chat.Id, _defaultMessages.WrongParams, update.Message.MessageId);
                return UpdateHandlingResult.Handled;
            }

            var dolls = _storage.GetByTime(input.ArgsInput);
            var message = _generator.CreateTimerMessage(dolls, _defaultMessages.CantFind);

            await Send(update.Message.Chat.Id, message, update.Message.MessageId);
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