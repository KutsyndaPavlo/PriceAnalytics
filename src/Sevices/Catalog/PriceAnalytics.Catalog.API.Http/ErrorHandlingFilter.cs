using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Product;
using System.Net;

namespace PriceAnalytics.Catalog.API
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private static void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            if (context.RouteData.Values["controller"].ToString() == "Proposal")
            {
                if (exception is ProductNotFoundException)
                    SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
                else if (exception is SellerNotFoundException)
                    SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
                else if (exception is BusinessRuleValidationException)
                    SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            }
            
            else if (exception is ProductCanNotBeDeletedException)
                SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            else if (exception is BusinessRuleValidationException)
                SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            else if (exception is ProductNotFoundException)
                SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            else if (exception is SellerNotFoundException)
                SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            else if (exception is ProposalNotFoundException)
                SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            else if (exception is ProductConcurrentUpdateException)
                SetExceptionResult(context, exception, HttpStatusCode.Conflict);
            else
                context.Result = new JsonResult($"Something went wrong. Details: {context.Exception}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
        }

        private static void SetExceptionResult(
            ExceptionContext context,
            Exception exception,
            HttpStatusCode code)
        {
            context.Result = new JsonResult(exception.Message)
            {
                StatusCode = (int)code
            };
        }
    }
}
