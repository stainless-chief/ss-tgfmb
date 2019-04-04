using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FrontlineMaidBot
{
    public class FrontlineMaidBot : BotBase<FrontlineMaidBot>
    {
        private const string _unknownUpdateRespose = "I'm sorry {0}. I'm afraid I can't do that. ";

        public FrontlineMaidBot(IOptions<BotOptions<FrontlineMaidBot>> botOptions)
            : base(botOptions)
        {
        }


        public override async Task HandleUnknownUpdate(Update update)
        {
            if (update.Message.Chat.Type != ChatType.Private)
            {
                await Task.CompletedTask;
            }
            else
            {
                await Client.SendTextMessageAsync
                (
                    update.Message.Chat.Id,
                    string.Format(_unknownUpdateRespose, update.Message.From.FirstName),
                    replyToMessageId: update.Message.MessageId
                );
            }
        }

        public override Task HandleFaultedUpdate(Update update, Exception e)
        {
            return Task.CompletedTask;
        }
    }
}
