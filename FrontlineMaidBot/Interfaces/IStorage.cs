using FrontlineMaidBot.Models;
using System.Collections.Generic;

namespace FrontlineMaidBot.Interfaces
{
    public interface IStorage
    {
        IEnumerable<ProductionResult> GetDolls();
        IEnumerable<ProductionResult> GetEquipment();

        IEnumerable<string> GetPokeJokes();
        IEnumerable<string> GetSlapJokes();
        string GetHelp();        
    }
}
