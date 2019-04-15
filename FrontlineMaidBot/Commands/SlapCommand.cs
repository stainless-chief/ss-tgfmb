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
        private const string _commandName = "slap";
        private const string _dataPath = "Slap.json";

        private readonly List<string> _responses;
        private readonly Random _rnd;

        public SlapCommand(IStorage storage) : base(name: _commandName)
        {
            _responses = storage.Load<List<string>>(_dataPath);
            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = ParseInput(update);

            if(string.IsNullOrEmpty(input.ArgsInput))
                return UpdateHandlingResult.Handled;

            string msg = MasterHack(input.ArgsInput);

            if (string.IsNullOrEmpty(msg))
            {
                var num = _rnd.Next(0, _responses.Count - 1);
                msg = string.Format(_responses[num], input.ArgsInput);
            }

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                msg,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }

        private static string[] master = {"chief", "chiefNoir", "@chiefnoir" };
        private string MasterHack(string arguments)
        {
            if (master.Contains(arguments.ToLower()))
                return "I am his bespoke and beloved maid. I will not commit act of violence towards to him.";

            return null;
        }

    }
}