using FrontlineMaidBot.Interfaces;
using FrontlineMaidBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace FrontlineMaidBot.DAL
{
    public class Storage : IStorage
    {
        private const string _dataFolder = "Data";
        private const string _dollsFile = "Dolls.json";
        private const string _equipmentFile = "Equipment.json";
        private const string _helpFile = "Help.json";
        private const string _pokeFile = "Poke.json";
        private const string _slapFile = "Slap.json";

        private static T LoadFromFile<T>(string path)
        {
            var content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public IEnumerable<ProductionResult> GetDolls()
        {
            return LoadFromFile<List<ProductionResult>>(Path.Combine(_dataFolder, _dollsFile));
        }

        public IEnumerable<ProductionResult> GetEquipment()
        {
            return LoadFromFile<List<ProductionResult>>(Path.Combine(_dataFolder, _equipmentFile));
        }

        public IEnumerable<string> GetPokeJokes()
        {
            return LoadFromFile<List<string>>(Path.Combine(_dataFolder, _pokeFile));
        }

        public IEnumerable<string> GetSlapJokes()
        {
            return LoadFromFile<List<string>>(Path.Combine(_dataFolder, _slapFile));
        }

        public string GetHelp()
        {
            var help = LoadFromFile<List<string>>(Path.Combine(_dataFolder, _helpFile));

            return string.Join(Environment.NewLine, help);
        }
    }
}