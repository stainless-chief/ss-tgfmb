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
        private static readonly string[] _self = { "@FrontlineMaidBot", "FrontlineMaid", "G36" };        
        private const string _cantSlapMaster = "I am his bespoken and beloved maid. I will not commit an act of violence towards to him.";
        private const string _cantSlapHerself = "You are not a smart man, aren't you?";

        private const string _commandName = "slap";

        private readonly List<string> _responses;
        private readonly Random _rnd;

        private readonly IDefaultMessages _defaultMessages;

        public SlapCommand(IStorage storage, IDefaultMessages defaultMessages) : base(name: _commandName)
        {
            _defaultMessages = defaultMessages;

            _responses = storage.GetSlapJokes().ToList();

            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update?.Message?.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = ParseInput(update);
            if(string.IsNullOrEmpty(input?.ArgsInput))
            {
                await Send(update.Message.Chat.Id, _defaultMessages.WrongParams, update.Message.MessageId);
                return UpdateHandlingResult.Handled;
            }

            string msg = CreateMsg(input.ArgsInput);

            if(string.IsNullOrEmpty(msg))
                return UpdateHandlingResult.Handled;

            await Send(update.Message.Chat.Id, msg, update.Message.MessageId);
            return UpdateHandlingResult.Handled;
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