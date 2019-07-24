using FrontlineMaidBot.Models;
using System.Collections.Generic;

namespace FrontlineMaidBot.Interfaces
{
    public interface IStorage
    {
        IEnumerable<ProductionResult> GetByTime(string name);



        IEnumerable<ProductionResult> GetDollsByTime(string time);
        IEnumerable<ProductionResult> GetDollsByName(string name);

        IEnumerable<ProductionResult> GetEquipmentByTime(string time);
        IEnumerable<ProductionResult> GetEquipmentByName(string name);




        IEnumerable<string> GetPokeJokes();
        IEnumerable<string> GetSlapJokes();
        string GetHelp();        
    }
}
