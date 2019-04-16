using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class SlapCommand : CommandBase<BaseCommandArgs>
    {
        private static readonly string[] _master = { "chief", "chiefNoir", "@chiefnoir" };
        private static readonly string[] _self = { "@FrontlineMaidBot", "FrontlineMaid",  };

        private const string _commandName = "slap";
        private const string _masterHack = "I am his bespoked and beloved maid. I will not commit an act of violence towards to him.";
        private const string _selfHack = "Bedenke, dass du sterben musst.";

        private readonly List<string> _responses;
        private readonly Random _rnd;

        public SlapCommand(IStorage storage) : base(name: _commandName)
        {
            _responses = storage.GetSlapJokes().ToList();
            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = ParseInput(update);
            string msg = CreateMsg(input.ArgsInput);

            if(string.IsNullOrEmpty(msg))
                return UpdateHandlingResult.Handled;

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                msg,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }

        
        private string CreateMsg(string arguments)
        {
            if (string.IsNullOrEmpty(arguments))
                return null;

            if (_master.Contains(arguments.ToLower()))
                return _masterHack;

            if (_self.Contains(arguments.ToLower()))
                return _selfHack;

            var num = _rnd.Next(0, _responses.Count - 1);
            return string.Format(_responses[num], arguments);
        }

    }
}