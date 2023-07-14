using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

using StoreApp.Presentation.ActionFilters;
using StoreApp.Services.Abstract;
using StoreApp.WebAPI.Extensions;
using StoreApp.WebAPI.Utilities.Mapping;
using StoreApp.WebAPI.Utilities.Swagger;

using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureLogging();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.ConfigureControllers();

builder.Services.ConfigureActionFilters();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "StoreApp v1", Version = "v1", });
    options.SwaggerDoc("v2", new OpenApiInfo() { Title = "StoreApp v2", Version = "v2", });
    options.OperationFilter<SwaggerParameterFilters>();
    options.DocumentFilter<SwaggerVersionMapping>();
});

builder.Services.ConfigureVersioning();

builder.Services.RegisterRepository();
builder.Services.RegisterServices();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureCors();

builder.Services.AddCustomMediaTypes();

var app = builder.Build();

ILoggerService loggerService = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(loggerService);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

if(app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
