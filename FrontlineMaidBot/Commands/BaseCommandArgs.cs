using Telegram.Bot.Framework.Abstractions;

namespace FrontlineMaidBot.Commands
{
    public class BaseCommandArgs : ICommandArgs
    {
        public string RawInput { get; set; }
        public string ArgsInput { get; set; }
    }
}
