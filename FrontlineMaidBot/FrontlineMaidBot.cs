using Microsoft.Extensions.Logging;
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
        private const string _errorRespose = "I'm not feeling very well... please, inform @ChiefNoir about that. He will take care of me.";
        private readonly ILogger<FrontlineMaidBot> _logger;

        public FrontlineMaidBot(IOptions<BotOptions<FrontlineMaidBot>> botOptions, ILogger<FrontlineMaidBot> logger)
            : base(botOptions)
        {
            _logger = logger;
        }


        public override async Task HandleUnknownUpdate(Update update)
        {
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
            catch (Exception e)
            {
                await Client.SendTextMessageAsync
                    (
                        update.Message.Chat.Id,
                        string.Format(_errorRespose, update.Message.From.FirstName),
                        replyToMessageId: update.Message.MessageId
                    );

                _logger.LogError(e, $"[Username: {update.Message.Chat.Username}] [ChatUsername: {update.Message.Chat.Username}] [ChatType: {update.Message.Chat.Type}] [Message: {update.Message.Text}]", null);
            }
        }

        public override async Task HandleFaultedUpdate(Update update, Exception e)
        {
            _logger.LogError(e, $"[Username: {update.Message.Chat.Username}] [ChatUsername: {update.Message.Chat.Username}] [ChatType: {update.Message.Chat.Type}] [Message: {update.Message.Text}]", null);

            await Client.SendTextMessageAsync
                    (
                        update.Message.Chat.Id,
                        string.Format(_errorRespose, update.Message.From.FirstName),
                        replyToMessageId: update.Message.MessageId
                    );
        }
    }
}
