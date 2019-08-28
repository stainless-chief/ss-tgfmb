using FrontlineMaidBot.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
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
            _botClient.OnMessage += OnMessage;
            _botClient.OnReceiveError += OnReceiveError;
            _botClient.OnReceiveGeneralError += OnReceiveGeneralError;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => _botClient.StartReceiving());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // the ride will never ends
            throw new NotImplementedException();
        }

        private async void OnMessage(object sender, MessageEventArgs msg)
        {
            if (msg?.Message?.Chat == null)
                return;

            var response = _messageFactory.CreateResponse(msg?.Message);
            if (response == null)
                return;

            try
            {
                await _botClient.SendTextMessageAsync
                    (
                        msg.Message.Chat.Id,
                        response,
                        ParseMode.Html,
                        replyToMessageId: msg.Message.MessageId
                    );
            }
            catch (Exception ee)
            {
                // bot must not fail
                _logger.LogError(ee, "OnMessage failed", null);
            }
        }

        private void OnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            _logger.LogError(e.ApiRequestException, "OnReceiveError", null);
        }

        private void OnReceiveGeneralError(object sender, ReceiveGeneralErrorEventArgs e)
        {
            _logger.LogError(e.Exception, "OnReceiveError", null);
        }
    }
}