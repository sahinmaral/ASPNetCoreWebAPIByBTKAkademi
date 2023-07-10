﻿using Microsoft.EntityFrameworkCore;
using StoreApp.Presentation;
using NLog;

using StoreApp.Repositories.Abstract;
using StoreApp.Repositories.EFCore;
using StoreApp.Services;
using StoreApp.Services.Abstract;

using System.ComponentModel.Design;

namespace StoreApp.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreAppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SQLServerConnectionString"));
            });
        }

        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureLogging(this IServiceCollection services)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"./nlog.config"));
            services.AddSingleton<ILoggerService, LoggerManager>();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
                .AddCustomCSVFormatter()
                .AddXmlDataContractSerializerFormatters()
                .AddApplicationPart(typeof(AssemblyReference).Assembly)
                .AddNewtonsoftJson(opt =>
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );



        }
    }
}
