using FrontlineMaidBot.Extensions;
using FrontlineMaidBot.Helpers;
using FrontlineMaidBot.Interfaces;
using FrontlineMaidBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        private readonly List<ProductionResult> _productionResults;

        public Storage()
        {
            var dolls = LoadFromFile<List<ProductionResult>>(Path.Combine(_dataFolder, _dollsFile));
            var equipment = LoadFromFile<List<ProductionResult>>(Path.Combine(_dataFolder, _equipmentFile));

            _productionResults = dolls.Union(equipment).ToList();
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

        public IEnumerable<ProductionResult> GetByTime(string time)
        {
            var normalTime = Utils.NormalizeTime(time);

            if(string.IsNullOrEmpty(normalTime))
                return new List<ProductionResult>();

            return
                _productionResults.Where(x => x.Time == normalTime);
        }

        public IEnumerable<ProductionResult> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<ProductionResult>();

            //We need first excact run, to ensure uniqe 
            var ss = _productionResults.Where(x => x.Name == name);

            if (ss.Any())
                return ss;

            var normal = name.ToLower().Replace(new[] { " ", "-", "." }, "");

            //very deep run
            return _productionResults.Where(x => x.Lookup.Contains(normal));
        }

        

        private static T LoadFromFile<T>(string path)
        {
            var content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}