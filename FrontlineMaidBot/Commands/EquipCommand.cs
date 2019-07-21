using FrontlineMaidBot.Helpers;
using FrontlineMaidBot.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class EquipCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "equip";
        private const string _default = "I'm sorry. I can't find anything.";
        private readonly IStorage _storage;

        public EquipCommand(IStorage storage) : base(name: _commandName)
        {
            _storage = storage;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
                return UpdateHandlingResult.Handled;

            var input = base.ParseInput(update);
            var equip = _storage.GetEquipmentByTime(input.ArgsInput);
            var message = Utils.CreateResponse(equip, _default);

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                message,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }


    }
}