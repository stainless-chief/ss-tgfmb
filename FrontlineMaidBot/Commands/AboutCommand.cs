using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class AboutCommand : ICommand
    {
        public string CommandName => "/about";
        public IEnumerable<string> Aliases => new List<string> { };

        private readonly string _aboutResponse;

        public AboutCommand(IStorage storage)
        {
            _aboutResponse = string.Join
                (
                    $"{Environment.NewLine}",
                    storage.GetAbout()
                );
        }

        public string CreateResponse(Message message)
        {
            if (message?.Chat == null)
                return null;

            return _aboutResponse;
        }
    }
}
