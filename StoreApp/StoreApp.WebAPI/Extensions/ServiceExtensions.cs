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
using Microsoft.AspNetCore.Mvc.Versioning;
using StoreApp.WebAPI.Controllers;
using StoreApp.Presentation.Controllers;
using Marvin.Cache.Headers;

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
                config.CacheProfiles.Add("5MinsCacheProfile", new CacheProfile()
                {
                    Duration = 300
                });
            })
                .AddXmlDataContractSerializerFormatters()
                .AddCustomCSVFormatter()
                .AddApplicationPart(typeof(AssemblyReference).Assembly);
        }

        public static void ConfigureHTTPCacheHeaders(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(opt =>
            {
                opt.MaxAge = 70;
                opt.CacheLocation = CacheLocation.Public;
            });
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

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
                opt.Conventions.Controller<BooksController>().HasApiVersion(new ApiVersion(1,0));
                opt.Conventions.Controller<BooksV2Controller>().HasApiVersion(new ApiVersion(2,0));
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
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

        public static void ConfigureResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }
    }
}
