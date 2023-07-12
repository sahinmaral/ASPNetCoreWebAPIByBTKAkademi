using Microsoft.AspNetCore.Mvc.Filters;

using StoreApp.Entities.Models.Exceptions;
using StoreApp.Entities.Models.RequestFeatures;

namespace StoreApp.Presentation.ActionFilters
{
    public class PriceOutOfRangeCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var bookParameterCheckResult = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains(nameof(BookParameters))).Value;
            if(bookParameterCheckResult is not null)
            {
                var bookParameters = (BookParameters)bookParameterCheckResult;

                if (!(bookParameters.MaxPrice > bookParameters.MinPrice))
                    throw new PriceOutOfRangeBadRequestException();
            }

        }
    }
}
