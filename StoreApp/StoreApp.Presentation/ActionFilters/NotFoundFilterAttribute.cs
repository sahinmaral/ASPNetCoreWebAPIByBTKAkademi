using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using StoreApp.Entities.Models.Abstract;
using StoreApp.Entities.Models.Exceptions;
using StoreApp.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Presentation.ActionFilters
{
    public class NotFoundFilterAttribute<T> : IAsyncActionFilter where T : class, IEntity
    {
        private readonly IServiceManager _serviceManager;
        public NotFoundFilterAttribute(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var entityIdValue = context.ActionArguments.Values.FirstOrDefault();
            if (entityIdValue == null)
            {
                return;
            }

            var entityId = (int)entityIdValue;
            var anyEntity = await _serviceManager.BookService.GetByIdAsync(entityId);
            if (anyEntity is not null)
            {
                await next();
                return;
            }

            throw new BookNotFoundException(entityId);
        }
    }
}
