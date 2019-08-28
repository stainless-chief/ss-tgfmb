using FrontlineMaidBot.Extensions;
using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class SlapCommand : ICommand
    {
        private const string _cantSlapHerself = "You are not a smart man, aren't you?";
        private const string _cantSlapMaster = "I am his bespoken and beloved maid. I will not commit an act of violence towards to him.";

        private static readonly string[] _master = { "chief", "chiefnoir", "@chiefnoir" };
        private static readonly string[] _self = { "@FrontlineMaidBot", "FrontlineMaid", "G36", "maid" };

        private readonly IDefaultMessages _defaultMessages;
        private readonly List<string> _responses;
        private readonly Random _rnd;

        public string CommandName => "/slap";
        public IEnumerable<string> Aliases => new List<string> { };

        public SlapCommand(IStorage storage, IDefaultMessages defaultMessages)
        {
            _defaultMessages = defaultMessages;

            _responses = storage.GetSlapJokes().ToList();

            _rnd = new Random(DateTime.Now.Millisecond);
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

            string msg = CreateMsg(input);

            if (string.IsNullOrEmpty(msg))
                return null;

            return msg;
        }

        private string CreateMsg(string arguments)
        {
            if (string.IsNullOrEmpty(arguments))
                return null;

            if (_master.Contains(arguments.ToLower()))
                return _cantSlapMaster;

            if (_self.Contains(arguments.ToLower()))
                return _cantSlapHerself;

            var num = _rnd.Next(0, _responses.Count - 1);
            return string.Format(_responses[num], arguments);
        }

    }
}