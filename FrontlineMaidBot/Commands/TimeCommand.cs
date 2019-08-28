using FrontlineMaidBot.Extensions;
using FrontlineMaidBot.Interfaces;
using System.Collections.Generic;
using Telegram.Bot.Types;
using System;

namespace FrontlineMaidBot.Commands
{
    public class TimeCommand : ICommand
    {
        private readonly IDefaultMessages _defaultMessages;
        private readonly IResponseGenerator _generator;
        private readonly IStorage _storage;

        public string CommandName => "/time";
        public string Description => $"Format: '{CommandName} %hh:mm%' {Environment.NewLine}Get the possible outcome of production.";
        public IEnumerable<string> Aliases => new List<string> { "/t" };

        public TimeCommand(IStorage storage, IResponseGenerator generator, IDefaultMessages defaultMessages)
        {
            _storage = storage;
            _generator = generator;
            _defaultMessages = defaultMessages;
        }

        public string CreateResponse(Message message)
        {
            if (message?.Chat == null)
                return null;

            var input = message.GetCommandArgs();
            if (string.IsNullOrEmpty(input))
            {
                return _defaultMessages.WrongParams;
            }

            var dolls = _storage.GetByTime(input);
            return _generator.CreateTimerMessage(dolls, _defaultMessages.CantFind);

        }

    }
}