﻿using FrontlineMaidBot.Commands;
using FrontlineMaidBot.DAL;
using FrontlineMaidBot.Generator;
using FrontlineMaidBot.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace FrontlineMaidBot
{
    public class Startup
    {
        //TODO: logging is messy, I should do it properly
        private const string _botSettingsSection = "FrontlineMaidBot";
        private const string _logPath = @"Logs\log.log";

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(_logPath).CreateLogger();

            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Task.Factory.StartNew(async () =>
            {
                var botManager = app.ApplicationServices.GetRequiredService<IBotManager<FrontlineMaidBot>>();

                // make sure web hook is disabled so we can use long-polling
                await botManager.SetWebhookStateAsync(false);

                while (true)
                {
                    await Task.Delay(1000);
                    try
                    {
                        await botManager.GetAndHandleNewUpdatesAsync();
                    }
                    catch (Exception ee)
                    {
                        //loop should not fail
                        Log.Logger.Error(ee, "Main loop fails");
                    }
                }
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTelegramBot<FrontlineMaidBot>(Configuration.GetSection(_botSettingsSection))
                .AddUpdateHandler<StartCommand>()
                .AddUpdateHandler<PokeCommand>()
                .AddUpdateHandler<HelpCommand>()
                .AddUpdateHandler<IsGoodCommand>()
                .AddUpdateHandler<SlapCommand>()
                .AddUpdateHandler<TimeCommand>()
                .AddUpdateHandler<InfoCommand>()
                .AddUpdateHandler<AboutCommand>()
                .AddUpdateHandler<FeedbackCommand>()
                .Configure();

            services.AddLogging(configure => configure.AddSerilog());
            services.AddScoped<IStorage, Storage>();
            services.AddScoped<IResponseGenerator, ResponseGenerator>();
            services.AddScoped<IDefaultMessages, DefaultMessages>();
        }
    }
}
