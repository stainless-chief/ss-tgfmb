using FrontlineMaidBot.Models;
using System.Collections.Generic;

namespace FrontlineMaidBot.Interfaces
{
    public interface IStorage
    {
        /// <summary>Search ProductionResult items by name</summary>
        /// <param name="name">ProductionResult name</param>
        /// <returns>Result of empty collection</returns>
        IEnumerable<ProductionResult> GetByName(string name);

        /// <summary>Search ProductionResult items by time</summary>
        /// <param name="name">ProductionResult name</param>
        /// <returns>Result of empty collection</returns>
        IEnumerable<ProductionResult> GetByTime(string time);

        /// <summary>Search doll items by time</summary>
        /// <param name="name">ProductionResult name</param>
        /// <returns>Result of empty collection</returns>
        IEnumerable<ProductionResult> GetDollByTime(string time);

        /// <summary>Search doll items by time</summary>
        /// <param name="name">ProductionResult name</param>
        /// <returns>Result of empty collection</returns>
        IEnumerable<ProductionResult> GetEquipmentByTime(string time);

        /// <summary> Get preset of help message</summary>
        /// <returns>Text</returns>
        string GetHelp();

        /// <summary> Get preset of about message</summary>
        /// <returns>Text</returns>
        string GetAbout();

        /// <summary>Get collection of poke jokes</summary>
        /// <returns>Collection of poke jokes</returns>
        IEnumerable<string> GetPokeJokes();

        /// <summary>Get collection of slap jokes</summary>
        /// <returns>Collection of slap jokes</returns>
        IEnumerable<string> GetSlapJokes();

        /// <summary>Save feedback from user</summary>
        /// <param name="username">User name</param>
        /// <param name="chat">Chat name</param>
        /// <param name="text">Feedback</param>
        void SaveFeedback(string username, string chat, string text);
    }
}