using FrontlineMaidBot.Commands;
using FrontlineMaidBot.DAL;
using FrontlineMaidBot.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace FrontlineMaidBot
{
    public class Startup
    {
        private const string _botSettingsSection = "FrontlineMaidBot";

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("log.log").CreateLogger();

            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTelegramBot<FrontlineMaidBot>(Configuration.GetSection(_botSettingsSection))
                .AddUpdateHandler<StartCommand>()
                .AddUpdateHandler<EquipCommand>()
                .AddUpdateHandler<DollCommand>()
                .AddUpdateHandler<PokeCommand>()
                .AddUpdateHandler<HelpCommand>()
                .AddUpdateHandler<IsGoodCommand>()
                .Configure();

            services.AddLogging(configure => configure.AddSerilog());
            services.AddScoped<IStorage, Storage>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Task.Factory.StartNew(async () =>
            {
                var botManager = app.ApplicationServices.GetRequiredService<IBotManager<FrontlineMaidBot>>();

                // make sure webhook is disabled so we can use long-polling
                await botManager.SetWebhookStateAsync(false);

                while (1 == 1)
                {
                    await Task.Delay(3000);
                    await botManager.GetAndHandleNewUpdatesAsync();
                }
            }).ContinueWith(t =>
            {
                if (t.IsFaulted) throw t.Exception;
            });
        }

    }
}

