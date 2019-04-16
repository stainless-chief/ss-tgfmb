﻿using FrontlineMaidBot.Interfaces;
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
        private readonly string _helpResponse;

        public HelpCommand(IStorage storage) : base(name: _commandName)
        {
            _helpResponse = string.Join
                (
                    $"{Environment.NewLine}",
                    storage.GetHelp()
                );
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

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