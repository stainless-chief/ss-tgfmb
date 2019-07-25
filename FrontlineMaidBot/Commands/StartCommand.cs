using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using FrontlineMaidBot.Interfaces;

namespace FrontlineMaidBot.Commands
{
    public class StartCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "start";

        private readonly IDefaultMessages _defaultMessages;

        public StartCommand(IDefaultMessages defaultMessages) : base(name: _commandName)
        {
            _defaultMessages = defaultMessages;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                _defaultMessages.Greeting
            );

            return UpdateHandlingResult.Handled;
        }

    }
}
