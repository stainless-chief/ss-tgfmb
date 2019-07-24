using FrontlineMaidBot.Models;
using System.Collections.Generic;

namespace FrontlineMaidBot.Interfaces
{
    public interface IResponseGenerator
    {
        string CreateTimerMessage(IEnumerable<ProductionResult> items, string defaultResponse);
        string CreateSummaryMessage(ProductionResult item, string defaultResponse);
        string CreateInfoMessage(ProductionResult item, string defaultResponse);

        string CreateSuggestionMessage(IEnumerable<ProductionResult> items, string defaultResponse, string suggestion);
    }
}
