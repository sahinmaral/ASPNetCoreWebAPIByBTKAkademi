using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

using StoreApp.Entities.Enums;
using StoreApp.Entities.Models;
using StoreApp.Services.Abstract;

namespace StoreApp.Presentation.ActionFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _loggerService;

        public LogFilterAttribute(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _loggerService.Log(Log("OnActionExecuting", context.RouteData),LogTypes.Info);
        }

        private string Log(string modelName,RouteData routeData)
        {
            var logDetail = new LogDetail()
            {
                ModelName = modelName,
                Controller = routeData.Values["controller"],
                Action = routeData.Values["action"],
            };

            if(routeData.Values.Count >= 3)
            {
                logDetail.Id = routeData.Values["Id"];
            }

            return logDetail.ToString();
        }
    }
}
