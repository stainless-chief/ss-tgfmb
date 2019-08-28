using FrontlineMaidBot.Extensions;
using FrontlineMaidBot.Interfaces;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class DollCommand :  ICommand
    {
        private readonly IDefaultMessages _defaultMessages;
        private readonly IResponseGenerator _generator;
        private readonly IStorage _storage;

        public string CommandName => "/doll";
        public IEnumerable<string> Aliases => new List<string> { "/d" };

        public DollCommand(IStorage storage, IResponseGenerator generator, IDefaultMessages defaultMessages)
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

            var dolls = _storage.GetDollByTime(input);
            return _generator.CreateTimerMessage(dolls, _defaultMessages.CantFind);

        }

    }
}