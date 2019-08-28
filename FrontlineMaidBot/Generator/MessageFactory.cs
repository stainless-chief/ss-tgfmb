using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Generator
{
    public class MessageFactory : IMessageFactory
    {
        private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();
        private readonly string _helpText;

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

            _helpText = CreateHelp(commands);
        }

        public string CreateResponse(Message message)
        {
            if (message?.Text == null || !message.Text.StartsWith('/'))
                return null;

            var cmd = message.Text.Split(' ').FirstOrDefault();

            if (string.IsNullOrEmpty(cmd))
                return null;

            cmd = cmd.Split('@').FirstOrDefault();

            if (cmd == "/help")
                return _helpText;

            if (_commands.ContainsKey(cmd))
            {
                return _commands[cmd].CreateResponse(message);
            }

            return null;
        }

        private static string CreateHelp(IEnumerable<ICommand> commands)
        {
            var helpText = new StringBuilder();

            foreach (var item in commands.OrderBy(x=>x.CommandName))
            {
                if (item.CommandName == "/start")
                    continue;

                helpText.Append($"{item.CommandName}{Environment.NewLine}");

                if (item.Aliases.Any())
                {
                    helpText.Append($"\tAliases: {string.Join(',', item.Aliases)}{Environment.NewLine}");
                }

                helpText.Append($"\t{item.Description}{Environment.NewLine}{Environment.NewLine}");
            }

            return helpText.ToString();
        }
    }
}