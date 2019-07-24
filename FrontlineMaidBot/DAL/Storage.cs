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


        private readonly List<ProductionResult> _dolls = new List<ProductionResult>();
        private readonly List<ProductionResult> _equipment = new List<ProductionResult>();

        public Storage()
        {
            _dolls = LoadFromFile<List<ProductionResult>>(Path.Combine(_dataFolder, _dollsFile));
            _equipment = LoadFromFile<List<ProductionResult>>(Path.Combine(_dataFolder, _equipmentFile));
        }




        private static T LoadFromFile<T>(string path)
        {
            var content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(content);
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




        public IEnumerable<ProductionResult> GetDollsByTime(string time)
        {
            if (string.IsNullOrEmpty(time))
                return new List<ProductionResult>();

            var normalTime = Utils.NormalizeTime(time);

            return _dolls.Where(x => x.Time == normalTime);
        }

        public IEnumerable<ProductionResult> GetDollsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<ProductionResult>();

            var normal = name.ToLower().Replace(" ", string.Empty);

            return _dolls.Where(x => x.Lookup.Contains(normal));
        }

        public IEnumerable<ProductionResult> GetEquipmentByTime(string time)
        {
            if (string.IsNullOrEmpty(time))
                return new List<ProductionResult>();

            var normalTime = Utils.NormalizeTime(time);

            return _equipment.Where(x => x.Time == normalTime);
        }

        public IEnumerable<ProductionResult> GetEquipmentByName(string name)
        {
            throw new NotImplementedException();
        }




        public IEnumerable<ProductionResult> GetByTime(string time)
        {
            var normalTime = Utils.NormalizeTime(time);

            if(string.IsNullOrEmpty(normalTime))
                return new List<ProductionResult>();

            return
                _equipment.Where(x => x.Time == normalTime).Union(
                    _dolls.Where(x => x.Time == normalTime)

                    );
        }

        public IEnumerable<ProductionResult> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<ProductionResult>();

            //We need first excact run, to ensure uniqe 
            var ss = _dolls.Where(x => x.Name == name).Union(
                _equipment.Where(x => x.Name == name));

            if (ss.Any())
                return ss;

            var normal = name.ToLower().Replace(" ", string.Empty).Replace("-", string.Empty);

            //very deep run
            return _dolls.Where(x => x.Lookup.Contains(normal)).Union(
                _equipment.Where(x => x.Lookup.Contains(normal)));
        }
    }
}