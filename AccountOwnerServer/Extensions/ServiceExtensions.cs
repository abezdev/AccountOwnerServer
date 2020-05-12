using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace AccountOwnerServer.Extensions
{

    /*
     * An extension method is inherently the static method. They play a great role in 
     * .NET Core configuration because they increase the readability of our code for sure. 
     * What makes it different from the other static methods is that it accepts “this” as 
     * the first parameter, and “this” represents the data type of the object which uses 
     * that extension method. An extension method must be inside a static class. This kind 
     * of method extends the behavior of the types in .NET 
     * 
     * in this case.... ConfigureCors and ConfigureIISIntegration are the extension methods
     * 
     */
    public static class ServiceExtensions
    {
        //CORS (Cross-Origin Resource Sharing) is a mechanism that gives rights to the 
        //user to access resources from the server on a different domain. Because we will 
        //    use Angular as a client-side on a different domain than the server’s domain, 
        //    configuring CORS is mandatory
        public static void ConfigureCors(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()     //WithOrigins("http://www.something.com")
                    .AllowAnyMethod()                       //WithMethods("POST", "GET")
                    .AllowAnyHeader());                     //WithHeaders("accept", "content-type")
            });
        }

        //we need to configure an IIS integration which will help us with the IIS deployment
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        /*
         * By calling services.AddSingleton will create the service the first time you 
         * request it and then every subsequent request is calling the same instance of 
         * the service. This means that all components are sharing the same service every 
         * time they need it. You are using the same instance always
         */
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();

            /*
              By calling services.AddSingleton will create the service the first time you request it and then every 
              subsequent request is calling the same instance of the service. This means that all components are 
              sharing the same service every time they need it. You are using the same instance always
              
              By calling services.AddScoped will create the service once per request. That means whenever 
              we send the HTTP request towards the application, a new instance of the service is created

              By calling services.AddTransient will create the service each time the application request it. 
              This means that if during one request towards our application, multiple components need the service, 
              this service will be created again for every single component which needs it
            */

        }



        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
        }


        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }




    }
}
