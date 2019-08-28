using System.Linq;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Extensions
{
    public static class ExtMessage
    {
        public static string GetCommandArgs(this Message message)
        {
            if (string.IsNullOrEmpty(message?.Text))
                return null;

            var cmd = message.Text.Split(' ').FirstOrDefault();

            if (string.IsNullOrEmpty(cmd))
                return null;

            return message.Text.Remove(0, cmd.Length).TrimStart();
        }
    }
}
