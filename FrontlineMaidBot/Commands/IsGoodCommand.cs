using FrontlineMaidBot.Interfaces;
using FrontlineMaidBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class IsGoodCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "isgood";
        private const string _dataPath = "Dolls.json";
        private const string _default = "I'm sorry. I don't know.";

        private readonly List<ProductionResult> _dolls;

        public IsGoodCommand(IStorage storage) : base(name: _commandName)
        {
            _dolls = storage.Load<List<ProductionResult>>(_dataPath);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            var input = base.ParseInput(update);

            var doll = GetDoll(input.ArgsInput);

            string msg = _default;

            if(doll != null)
            {
                msg = $"[{doll.Category}] {doll.Name} {Environment.NewLine} {doll.Summary}";
            }

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                msg,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }


        public ProductionResult GetDoll(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return _dolls.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
