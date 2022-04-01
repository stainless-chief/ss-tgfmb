using FrontlineMaidBot.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FrontlineMaidBot.Service
{
    public class BotService : IHostedService
    {
        private const string _configToken = "ApiToken";

        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<BotService> _logger;
        private readonly IMessageFactory _messageFactory;

        public BotService(IMessageFactory messageFactory, ILogger<BotService> logger, IConfiguration config)
        {
            _messageFactory = messageFactory;
            _logger = logger;

            var token = config.GetValue<string>(_configToken);

            _botClient = new TelegramBotClient(token);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            return Task.Run(() => _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions), cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // the ride will never ends
            throw new NotImplementedException();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;

            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            var response = _messageFactory.CreateResponse(update.Message);
            if (response == null)
                return;

            try
            {
                await _botClient.SendTextMessageAsync
                    (
                        update.Message.Chat.Id,
                        response,
                        ParseMode.Html,
                        replyToMessageId: update.Message.MessageId
                    );
            }
            catch (Exception ee)
            {
                // bot must not fail
                _logger.LogError(ee, "OnMessage failed", null);
            }

        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}