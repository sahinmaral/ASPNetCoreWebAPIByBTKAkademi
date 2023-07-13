using Microsoft.AspNetCore.Mvc.Filters;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models.Exceptions;

namespace StoreApp.Presentation.ActionFilters
{
    public class PriceOutOfRangeCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var linkParameterCheckResult = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains(nameof(LinkParameters))).Value;
            if(linkParameterCheckResult is not null)
            {
                var bookParameters = ((LinkParameters)linkParameterCheckResult).BookParameters;

                if (!(bookParameters.MaxPrice > bookParameters.MinPrice))
                    throw new PriceOutOfRangeBadRequestException();
            }

        }
    }
}
