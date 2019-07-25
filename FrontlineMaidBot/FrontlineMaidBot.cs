using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FrontlineMaidBot.Interfaces;

namespace FrontlineMaidBot
{
    public class FrontlineMaidBot : BotBase<FrontlineMaidBot>
    {
        private readonly ILogger<FrontlineMaidBot> _logger;
        private readonly IDefaultMessages _defaultMessages;

        public FrontlineMaidBot(IOptions<BotOptions<FrontlineMaidBot>> botOptions, ILogger<FrontlineMaidBot> logger, IDefaultMessages defaultMessages)
            : base(botOptions)
        {
            _logger = logger;
            _defaultMessages = defaultMessages;

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
                            _defaultMessages.WrongCommand,
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
                            _defaultMessages.ErrorHappens
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

            if (update?.Message?.Chat == null)
                return;

            try
            {
                await Client.SendTextMessageAsync
                        (
                            update.Message.Chat.Id,
                            _defaultMessages.ErrorHappens,
                            replyToMessageId: update.Message.MessageId
                        );
            }
            catch (Exception ee)
            {
                _logger.LogError(ee, $"[Username: {update?.Message?.Chat?.Username}]"
                    + $"[ChatUsername: {update?.Message?.Chat?.Username}] "
                    + $"[ChatType: {update?.Message?.Chat?.Type}] "
                    + $"[Message: {update?.Message?.Text}]", null);
            }
        }
    }
}
