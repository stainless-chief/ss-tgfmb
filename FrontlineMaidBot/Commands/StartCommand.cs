using FrontlineMaidBot.Interfaces;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class StartCommand :  ICommand
    {
        private readonly IDefaultMessages _defaultMessages;

        public string CommandName => "/start";
        public string Description => "Initiate the conversation.";
        public IEnumerable<string> Aliases => new List<string> { };

        public StartCommand(IDefaultMessages defaultMessages) 
        {
            _defaultMessages = defaultMessages;
        }

        public string CreateResponse(Message message)
        {
            if (message?.Chat == null)
                return null;

            return _defaultMessages.Greeting;
        }

    }
}