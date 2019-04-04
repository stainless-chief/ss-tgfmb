using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class PokeCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "poke";
        private const string _dataPath = "Poke.json";

        private readonly List<string> _responses;
        private readonly Random _rnd;

        public PokeCommand(IStorage storage) : base(name: _commandName)
        {
            _responses = storage.Load<List<string>>(_dataPath);
            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            var num = _rnd.Next(0, _responses.Count - 1);
            var message = _responses[num];
            
            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                message,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }

    }
}