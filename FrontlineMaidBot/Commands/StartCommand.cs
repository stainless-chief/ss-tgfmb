using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class StartCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "start";
        private const string _message = "Guten Tag, commander.";

        public StartCommand() : base(name: _commandName)
        {
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                _message
            );

            return UpdateHandlingResult.Handled;
        }
    }
}
