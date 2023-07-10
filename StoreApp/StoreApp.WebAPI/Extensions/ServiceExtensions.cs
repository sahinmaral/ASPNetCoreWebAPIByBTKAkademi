﻿using Microsoft.EntityFrameworkCore;

using NLog;

using StoreApp.Repositories.Abstract;
using StoreApp.Repositories.EFCore;
using StoreApp.Services;
using StoreApp.Services.Abstract;

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
    }
}
