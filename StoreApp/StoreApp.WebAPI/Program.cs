using AspNetCoreRateLimit;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

using StoreApp.Services.Abstract;
using StoreApp.WebAPI.Extensions;
using StoreApp.WebAPI.Utilities.Mapping;

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
builder.Services.ConfigureSwagger();

builder.Services.ConfigureVersioning();
builder.Services.RegisterRepository();
builder.Services.RegisterServices();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.AddCustomMediaTypes();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHTTPCacheHeaders();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);


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
app.UseIpRateLimiting();
app.UseCors();
app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
