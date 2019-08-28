using Telegram.Bot.Types;

namespace FrontlineMaidBot.Interfaces
{
    /// <summary> Message Response Factory</summary>
    public interface IMessageFactory
    {
        /// <summary> Create response for message</summary>
        /// <param name="message">User message</param>
        /// <returns>Response for message or null</returns>
        string CreateResponse(Message message);
    }
}
