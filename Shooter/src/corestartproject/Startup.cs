using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using Core;
using Hubs;
using GameLogic.GameController;
using Core.Services.Games;
using GameServices;
using Core.Logic.Controllers;
using Shooter.Web.Hubs;
using CommonEnveroment;

namespace Core.Start
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IEnveroment GameEnveroment { get; set; }
        public IGameService GameService { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                            .SetBasePath(env.ContentRootPath)
                                            .AddJsonFile("appsettings.json")
                                            .AddEnvironmentVariables();
            Configuration = builder.Build();
            GameEnveroment = new GameEnveroment(typeof(HubProcessUnit),typeof(PlayerControl));
            GameService = new GameService(GameEnveroment);
            GameEnveroment.SetGameService(GameService);

            E.env = GameEnveroment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSignalR(options => options.Hubs.EnableDetailedErrors = true);
            services.AddSingleton(Configuration);
            services.AddSingleton(GameService);
            services.AddSingleton(GameEnveroment);

            services.AddScoped<IRoomProcessUnit,GameUnit>();
            services.AddScoped<IPlayerController, PlayerControl>();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseCors("AllowAll");
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);
            app.UseMvc(RouteMap);
            app.UseWebSockets();
            app.UseSignalR();
            app.Use(SocketHandler.Acceptor);
        }


        private void RouteMap(IRouteBuilder routerBuilder)
        {
            routerBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }

    }

    
}

namespace System
{
    public static class E
    {
        public static IEnveroment env { get; set; }
    }
}
