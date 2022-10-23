using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PriceAnalytics.ApiGateway
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            // ToDo log exception.Message
            context.Result = new JsonResult($"Gateway. Something went wrong. Details: {context.Exception}")
            {
                StatusCode = StatusCodes.Status500InternalServerError                
            };
            context.ExceptionHandled = true;
        }
    }
}
