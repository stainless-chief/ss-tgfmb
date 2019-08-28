using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly string _helpResponse;

        public string CommandName => "/help";
        public IEnumerable<string> Aliases => new List<string> { };

        public HelpCommand(IStorage storage)
        {
            _helpResponse = string.Join
                (
                    $"{Environment.NewLine}",
                    storage.GetHelp()
                );
        }

        public string CreateResponse(Message message)
        {
            if (message?.Chat == null)
                return null;

            return _helpResponse;
        }

    }
}