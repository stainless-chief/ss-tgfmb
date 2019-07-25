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
    public class PokeCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "poke";

        private readonly List<string> _responses;
        private readonly Random _rnd;

        public PokeCommand(IStorage storage) : base(name: _commandName)
        {
            _responses = storage.GetPokeJokes().ToList();

            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            var num = _rnd.Next(0, _responses.Count - 1);
            var message = _responses[num];

            await Send(update.Message.Chat.Id, message, update.Message.MessageId);
            return UpdateHandlingResult.Handled;
        }

        private Task<Message> Send(long chatId, string message, int messageId)
        {
            return Bot.Client.SendTextMessageAsync
            (
                chatId,
                message,
                Telegram.Bot.Types.Enums.ParseMode.Html,
                replyToMessageId: messageId
            );
        }
    }
}