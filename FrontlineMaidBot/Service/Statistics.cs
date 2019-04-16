using FrontlineMaidBot.Interfaces;
using System;
using System.Reflection;

namespace FrontlineMaidBot.Service
{
    public class Statistics : IStatistics
    {
        private readonly DateTime _initTimeUtc;

        public TimeSpan Uptime
        {
            get
            {
                return DateTime.UtcNow - _initTimeUtc;
            }
        }

        public string Version { get; private set; }


        public Statistics()
        {
            _initTimeUtc = DateTime.UtcNow;
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        public string GenerateReport()
        {
            return $"Uptime: {Uptime} {Environment.NewLine}"
                 + $"Version: {Version} {Environment.NewLine}"
                ;
        }
    }
}
