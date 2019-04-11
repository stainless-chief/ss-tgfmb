using FrontlineMaidBot.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace FrontlineMaidBot.DAL
{
    public class Storage : IStorage
    {
        private const string _dataFolder = "Data";

        public T Load<T>(string name)
        {
            return LoadFromFile<T>(Path.Combine(_dataFolder, name));
        }

        private static T LoadFromFile<T>(string path)
        {
            var content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}