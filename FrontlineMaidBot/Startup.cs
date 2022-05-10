using FrontlineMaidBot.Commands;
using FrontlineMaidBot.DAL;
using FrontlineMaidBot.Generator;
using FrontlineMaidBot.Interfaces;
using FrontlineMaidBot.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FrontlineMaidBot
{
    public class Startup
    {
        //TODO: logging is messy, I should do it properly        
        private const string _logPath = @"log.log";

        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(_logPath).CreateLogger();

            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<BotService>();

            services.AddScoped<ICommand, AboutCommand>();
            services.AddScoped<ICommand, InfoCommand>();
            services.AddScoped<ICommand, IsGoodCommand>();
            services.AddScoped<ICommand, PokeCommand>();
            services.AddScoped<ICommand, SlapCommand>();
            services.AddScoped<ICommand, StartCommand>();
            services.AddScoped<ICommand, TimeCommand>();
            services.AddScoped<ICommand, DollCommand>();
            services.AddScoped<ICommand, EquipmentCommand>();

            services.AddScoped<IResponseGenerator, ResponseGenerator>();
            services.AddScoped<IDefaultMessages, DefaultMessages>();

            services.AddSingleton<IMessageFactory, MessageFactory>();
            services.AddSingleton<IStorage, Storage>();
            services.AddSingleton(Configuration);            
        }
    }
}
