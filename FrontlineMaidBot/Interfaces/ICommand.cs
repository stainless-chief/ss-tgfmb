using System.Collections.Generic;
using Telegram.Bot.Types;

namespace FrontlineMaidBot.Interfaces
{
    /// <summary> Base bot command </summary>
    public interface ICommand
    {
        /// <summary> Get command name, started with / (slash)</summary>
        string CommandName { get; }

        /// <summary> Get command friendly description </summary>
        string Description { get; }

        /// <summary> Get all command aliases </summary>
        IEnumerable<string> Aliases { get; }

        /// <summary> Create response on command</summary>
        /// <param name="message">User's message</param>
        /// <returns>Response or null</returns>
        string CreateResponse(Message message);
    }
}
