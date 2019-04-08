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
            //Console.WriteLine("FrontlineMaidBot - Ok");
            //Console.WriteLine("API:" + botOptions.Value.ApiToken);
        }


        public override async Task HandleUnknownUpdate(Update update)
        {
            //Console.WriteLine(update.Message.Text);

            try
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
            catch(Exception ee)
            {
                throw ee;
            }
        }

        public override Task HandleFaultedUpdate(Update update, Exception e)
        {
            try
            {
                return Task.CompletedTask;
            }
            catch(Exception ee)
            {
                throw ee;
            }
        }
    }
}
