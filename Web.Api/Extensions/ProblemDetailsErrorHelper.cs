using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Extensions
{
    public static class ProblemDetailsErrorHelper
    {
        //https://andrewlock.net/handling-web-api-exceptions-with-problemdetails-middleware/#:~:text=ProblemDetails%20and%20the%20%5BApiController%5D%20attribute,%3E%3D%20400)%20to%20ProblemDetails%20.
        //https://lurumad.github.io/problem-details-an-standard-way-for-specifying-errors-in-http-api-responses-asp.net-core

        public static ProblemDetailsException ProblemDetailsError(ModelStateDictionary modelState)
        {
            var problemDetailsValidation = new ValidationProblemDetails(modelState);
            problemDetailsValidation.Status = 400;
            return new ProblemDetailsException(problemDetailsValidation);
        }
    }
}
