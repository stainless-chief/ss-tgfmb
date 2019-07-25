using FrontlineMaidBot.Models;
using System.Collections.Generic;

namespace FrontlineMaidBot.Interfaces
{
    /// <summary> Text generator for requests</summary>
    public interface IResponseGenerator
    {
        /// <summary> Generate message about production time of <c>items</c>, which can be produced at <c>time</c> or return defaultResponse</summary>
        /// <param name="items">Items, from which the message will be generated</param>
        /// <param name="defaultResponse">Default response it items is null or empty</param>
        /// <returns>Text message with HTML tags</returns>
        string CreateTimerMessage(IEnumerable<ProductionResult> items, string defaultResponse);

        /// <summary> Generate quick summary message about <c>item</c> or return defaultResponse</summary>
        /// <param name="items">Item, from which the message will be generated</param>
        /// <param name="defaultResponse">Default response it item is null</param>
        /// <returns>Text message with HTML tags</returns>
        string CreateSummaryMessage(ProductionResult item, string defaultResponse);

        /// <summary> Generate information message about <c>item</c> or return defaultResponse</summary>
        /// <param name="items">Item, from which the message will be generated</param>
        /// <param name="defaultResponse">Default response it item is null</param>
        /// <returns>Text message with HTML tags</returns>
        string CreateInfoMessage(ProductionResult item, string defaultResponse);

        /// <summary> Generate information message with Names of items</summary>
        /// <param name="items">Item, from which the Name will be taken</param>
        /// <param name="defaultResponse">Default response it item is null</param>
        /// <param name="suggestion">The start of the message</param>
        /// <returns>Text message with HTML tags</returns>
        string CreateSuggestionMessage(IEnumerable<ProductionResult> items, string defaultResponse, string suggestion);
    }
}
