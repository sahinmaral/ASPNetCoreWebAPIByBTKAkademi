using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

using StoreApp.Entities.Enums;
using StoreApp.Entities.Models;
using StoreApp.Entities.Models.Exceptions;
using StoreApp.Services.Abstract;

using System.Net;

namespace StoreApp.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication application,ILoggerService loggerService)
        {

            application.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            BadRequestException => StatusCodes.Status400BadRequest,
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        loggerService.Log($"Something went wrong : {contextFeature.Error}", LogTypes.Error);
                        await context.Response.WriteAsync(
                            new ErrorDetail() 
                            { 
                                StatusCode = context.Response.StatusCode, 
                                Message = contextFeature.Error.Message
                            }.ToString()
                            );
                    }
                });
            });
        }
    }
}
