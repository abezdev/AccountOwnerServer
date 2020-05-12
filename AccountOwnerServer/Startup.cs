using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccountOwnerServer.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;

namespace AccountOwnerServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Setting up the configuration for a logger service
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //methods we created in Extensions folder
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();  //Every time we want to use a logger service, 
                                                //all we need to do is to inject it into the constructor 
                                                //of the class that is going to use that service. .NET Core 
                                                //will serve that service from the IOC container and all of 
                                                //its features will be available to use. This type of injecting 
                                                //objects is called Dependency Injection
            services.ConfigureMySqlContext(Configuration);
            services.ConfigureRepositoryWrapper();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //add different middleware components to the application’s request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //enables using static files for the request. If we don’t set a path to the static files, 
            //it will use a wwwroot folder in our solution explorer by default.
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            //will forward proxy headers to the current request. This will help us during the Linux deployment
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
