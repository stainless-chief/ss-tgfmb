using FrontlineMaidBot.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class FeedbackCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "feedback";

        private readonly IDefaultMessages _defaultMessages;
        private readonly IStorage _storage;

        public FeedbackCommand(IStorage storage, IDefaultMessages defaultMessages) : base(name: _commandName)
        {
            _defaultMessages = defaultMessages;
            _storage = storage;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = ParseInput(update);
            if(string.IsNullOrEmpty(input?.ArgsInput))
            {
                await Send(update.Message.Chat.Id, _defaultMessages.EmptyParams, update.Message.MessageId);
                return UpdateHandlingResult.Handled;
            }

            _storage.SaveFeedback(update?.Message?.Chat?.Username, update?.Message?.Chat?.Type.ToString(), input.ArgsInput);

            await Send(update.Message.Chat.Id, _defaultMessages.EverythingWentGood, update.Message.MessageId);
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
