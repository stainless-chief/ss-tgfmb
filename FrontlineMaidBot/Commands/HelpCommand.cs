using FrontlineMaidBot.Interfaces;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class HelpCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "help";
        private readonly string _helpResponse;

        public HelpCommand(IStorage storage) : base(name: _commandName)
        {
            _helpResponse = string.Join
                (
                    $"{Environment.NewLine}",
                    storage.GetHelp()
                );
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            await Send(update.Message.Chat.Id, _helpResponse, update.Message.MessageId);
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