using Microsoft.AspNetCore.Mvc;

using StoreApp.Presentation.ActionFilters;
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
builder.Services.AddSwaggerGen();

builder.Services.RegisterRepository();
builder.Services.RegisterServices();

builder.Services.ConfigureDbContext(builder.Configuration);
var app = builder.Build();

ILoggerService loggerService = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(loggerService);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
