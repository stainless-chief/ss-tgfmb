using FrontlineMaidBot.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Generator
{
    public class MessageFactory : IMessageFactory
    {
        private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public MessageFactory(IEnumerable<ICommand> commands)
        {
            foreach (var cmd in commands)
            {
                _commands.Add(cmd.CommandName, cmd);

                foreach (var alias in cmd.Aliases)
                {
                    _commands.Add(alias, cmd);
                }
            }
        }

        public string CreateResponse(Message message)
        {
            var cmd = message.Text.Split(' ').FirstOrDefault();

            if (string.IsNullOrEmpty(cmd))
                return null;

            if (_commands.ContainsKey(cmd))
            {
                return _commands[cmd].CreateResponse(message);
            }

            return null;
        }
    }
}