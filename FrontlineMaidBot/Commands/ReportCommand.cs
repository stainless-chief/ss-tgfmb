using FrontlineMaidBot.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class ReportCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "report";
        private readonly IStatistics _statistics;

        public ReportCommand(IStatistics statistics) : base(name: _commandName)
        {
            _statistics = statistics;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                _statistics.GenerateReport(),
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }

    }
}
