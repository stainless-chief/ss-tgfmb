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
        private const string _unknownUpdateRespose = "I'm sorry. I'm afraid I can't do that.";
        private const string _faultedUpdateRespose = "I'm not feeling very well... please, inform @ChiefNoir about that. He will take care of me.";
        private const string _unknownUpdateErrorRespose = "I'm not feeling very good... please, inform @ChiefNoir about that. He will take care of me.";
        private readonly ILogger<FrontlineMaidBot> _logger;

        public FrontlineMaidBot(IOptions<BotOptions<FrontlineMaidBot>> botOptions, ILogger<FrontlineMaidBot> logger)
            : base(botOptions)
        {
            _logger = logger;

            _logger.Log(LogLevel.Information, "Let's go", null);

            Client.OnReceiveError += Client_OnReceiveError;
            Client.OnReceiveGeneralError += Client_OnReceiveGeneralError;
        }


        private void Client_OnReceiveGeneralError(object sender, Telegram.Bot.Args.ReceiveGeneralErrorEventArgs e)
        {            
            _logger.LogError(e.Exception, "OnReceiveGeneralError", null);
        }

        private void Client_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            _logger.LogError(e.ApiRequestException, "OnReceiveError", null);
        }

        public override async Task HandleUnknownUpdate(Update update)
        {
            try
            {
                if (update?.Message?.Chat?.Type != ChatType.Private)
                {
                    await Task.CompletedTask;
                }
                else
                {
                    await Client.SendTextMessageAsync
                        (
                            update.Message.Chat.Id,
                            _unknownUpdateRespose,
                            replyToMessageId: update.Message.MessageId
                        );
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[Username: {update?.Message?.Chat?.Username}]"
                    + $"[ChatUsername: {update?.Message?.Chat?.Username}] "
                    + $"[ChatType: {update?.Message?.Chat?.Type}] "
                    + $"[Message: {update?.Message?.Text}]", null);

                if (update != null && update.Message != null && update.Message.Chat != null)
                {
                    await Client.SendTextMessageAsync
                        (
                            update.Message.Chat.Id,
                            _unknownUpdateErrorRespose
                        );
                }
            }
        }

        public override async Task HandleFaultedUpdate(Update update, Exception e)
        {
            _logger.LogError(e, $"[Username: {update?.Message?.Chat?.Username}]"
                    + $"[ChatUsername: {update?.Message?.Chat?.Username}] "
                    + $"[ChatType: {update?.Message?.Chat?.Type}] "
                    + $"[Message: {update?.Message?.Text}]", null);

            await Client.SendTextMessageAsync
                    (
                        update.Message.Chat.Id,
                        _faultedUpdateRespose,
                        replyToMessageId: update.Message.MessageId
                    );
        }
    }
}
