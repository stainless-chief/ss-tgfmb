using FrontlineMaidBot.Helpers;
using FrontlineMaidBot.Interfaces;
using FrontlineMaidBot.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Commands
{
    public class EquipCommand : CommandBase<BaseCommandArgs>
    {
        private const string _commandName = "equip";
        private const string _dataPath = "Equipment.json";
        private const string _default = "I'm sorry. I can't find anything.";

        private readonly List<ProductionResult> _equip;

        public EquipCommand(IStorage storage) : base(name: _commandName)
        {
            _equip = storage.Load<List<ProductionResult>>(_dataPath);
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, BaseCommandArgs args)
        {
            var input = base.ParseInput(update);

            var equip = GetEquip(input.ArgsInput);
            var message = Utils.CreateResponse(equip, _default);

            await Bot.Client.SendTextMessageAsync
            (
                update.Message.Chat.Id,
                message,
                replyToMessageId: update.Message.MessageId
            );

            return UpdateHandlingResult.Handled;
        }


        private IEnumerable<ProductionResult> GetEquip(string time)
        {
            if (string.IsNullOrEmpty(time))
                return new List<ProductionResult>();

            return _equip.Where(x => x.Time == Utils.NormalizeTime(time));
        }
    }
}