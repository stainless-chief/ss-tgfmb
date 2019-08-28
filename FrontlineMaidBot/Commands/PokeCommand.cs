using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class PokeCommand : ICommand
    {
        private readonly List<string> _responses;
        private readonly Random _rnd;

        public string CommandName => "/poke";
        public string Description => "Don't tease me.";
        public IEnumerable<string> Aliases => new List<string> { "/p" };

        public PokeCommand(IStorage storage) 
        {
            _responses = storage.GetPokeJokes().ToList();

            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public string CreateResponse(Message message)
        {
            if (message?.Chat == null)
                return null;

            var num = _rnd.Next(0, _responses.Count - 1);
            return _responses[num];
        }
       
    }
}