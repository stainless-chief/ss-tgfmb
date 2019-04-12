using System.IO;
using FrontlineMaidBot.Helpers;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace FrontlineMaidBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Utils.DoDirtyWork();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
