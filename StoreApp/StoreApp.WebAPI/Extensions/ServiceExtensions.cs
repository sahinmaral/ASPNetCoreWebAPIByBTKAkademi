using Microsoft.EntityFrameworkCore;
using StoreApp.Presentation;
using NLog;

using StoreApp.Repositories.Abstract;
using StoreApp.Repositories.EFCore;
using StoreApp.Services;
using StoreApp.Services.Abstract;

using System.ComponentModel.Design;
using StoreApp.Presentation.ActionFilters;
using StoreApp.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

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
            services.AddScoped<IDataShaper<BookDto>, DataShaper<BookDto>>();
            services.AddScoped<IBookLinks, BookLinks>();
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
                .AddXmlDataContractSerializerFormatters()
                .AddCustomCSVFormatter()
                .AddApplicationPart(typeof(AssemblyReference).Assembly);
                //.AddNewtonsoftJson(opt =>
                //    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //);
        }

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped(typeof(ValidationFilterAttribute));
            services.AddSingleton(typeof(LogFilterAttribute));
            services.AddScoped(typeof(NotFoundFilterAttribute<>));
            services.AddScoped(typeof(PriceOutOfRangeCheckAttribute));
            services.AddScoped(typeof(ValidateMediaTypeAttribute));
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("X-Pagination");
                });
            });
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config
                .OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if(systemTextJsonOutputFormatter is not null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.storeapp.hateoas+json");

                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.storeapp.apiroot+json");
                }

                var xmlOutputFormatter = config
                .OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?
                .FirstOrDefault();

                if (xmlOutputFormatter is not null)
                {
                    xmlOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.storeapp.hateoas+xml");

                    xmlOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.storeapp.apiroot+xml");
                }

            });
        }
    }
}
