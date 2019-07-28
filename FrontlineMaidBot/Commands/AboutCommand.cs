using FrontlineMaidBot.Interfaces;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;


namespace FrontlineMaidBot.Commands
{
    public class AboutCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "about";
        private readonly string _aboutResponse;

        public AboutCommand(IStorage storage) : base(name: _commandName)
        {
            _aboutResponse = string.Join
                (
                    $"{Environment.NewLine}",
                    storage.GetAbout()
                );
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            await Send(update.Message.Chat.Id, _aboutResponse, update.Message.MessageId);
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
