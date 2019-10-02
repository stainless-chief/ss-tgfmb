using FrontlineMaidBot.Extensions;
using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class FeedbackCommand : ICommand
    {
        private readonly IDefaultMessages _defaultMessages;
        private readonly IStorage _storage;

        public string CommandName => "/feedback";
        public string Description => $"Format: '{CommandName} %text%'{Environment.NewLine}Give bug report or suggest improvement.";
        public IEnumerable<string> Aliases => new List<string> { };

        public FeedbackCommand(IStorage storage, IDefaultMessages defaultMessages)
        {
            _defaultMessages = defaultMessages;
            _storage = storage;
        }

        public string CreateResponse(Message message)
        {
            if (message?.Chat == null)
                return null;

            return _defaultMessages.TurnedOff;
        }

    }
}
