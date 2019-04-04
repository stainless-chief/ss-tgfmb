using FrontlineMaidBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class HelpCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "help";
        private const string _dataPath = "Help.json";

        private readonly string _helpResponse;

        public HelpCommand(IStorage storage) : base(name: _commandName)
        {
            _helpResponse = string.Join
                (
                    $"{Environment.NewLine}",
                    storage.Load<List<string>>(_dataPath)
                );
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                _helpResponse,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }

    }
}