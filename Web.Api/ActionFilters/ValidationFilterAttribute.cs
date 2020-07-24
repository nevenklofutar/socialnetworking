using Contracts;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.Api.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly ILoggerManager _logger;
        public ValidationFilterAttribute(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param == null)
            {
                _logger.LogError($"Object sent from client is null. Controller: {controller}, action: { action}");
                //context.Result = new BadRequestObjectResult($"Object is null. Controller: { controller }, action: { action} ");
                //return;
                throw new ProblemDetailsException((int)HttpStatusCode.BadRequest, 
                    $"Object is null. Controller: { controller }, action: { action} ");
            }

            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the object. Controller: { controller}, action: { action}");
                
                //context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                var problemDetailsValidation = new ValidationProblemDetails(context.ModelState);
                problemDetailsValidation.Status = 422;
                throw new ProblemDetailsException(problemDetailsValidation);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
