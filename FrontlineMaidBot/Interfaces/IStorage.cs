using FrontlineMaidBot.Models;
using System.Collections.Generic;

namespace FrontlineMaidBot.Interfaces
{
    public interface IStorage
    {
        IEnumerable<ProductionResult> GetByTime(string time);
        IEnumerable<ProductionResult> GetByName(string name);

        IEnumerable<string> GetPokeJokes();
        IEnumerable<string> GetSlapJokes();

        string GetHelp();        
    }
}
