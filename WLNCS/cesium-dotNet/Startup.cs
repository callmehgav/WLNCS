using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;

namespace cesium_dotNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSpaStaticFiles(config => config.RootPath = "wwwroot");
            services.AddSpaStaticFiles(config => config.RootPath = "../cesium-angular/dist");
            services.AddSingleton(typeof(MessageBus), new MessageBus());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //taken from default startup
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello from Startup!");
                });
            });*/

            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var socket = await context.WebSockets.AcceptWebSocketAsync();
                        var squareService = (MessageBus)app.ApplicationServices.GetService(typeof(MessageBus));
                        await squareService.AddUser(socket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });
            app.UseSpa(config =>
            {
                config.Options.SourcePath = "../cesium-angular";
                if (env.IsDevelopment())
                {
                    config.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
