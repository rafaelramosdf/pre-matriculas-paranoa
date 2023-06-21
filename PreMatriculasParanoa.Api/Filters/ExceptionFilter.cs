using PreMatriculasParanoa.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Net.Mime;

namespace PreMatriculasParanoa.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errors = new List<string>();

            errors.Add(context.Exception.Message);

            if (!string.IsNullOrEmpty(context.Exception.InnerException?.Message))
                errors.Add(context.Exception.InnerException.Message);

            var commandResult = new CommandResult(StatusCodes.Status500InternalServerError);
            commandResult.SetErrors(errors);

            context.Result = new JsonResult(commandResult)
            {
                StatusCode = commandResult.StatusCode,
                ContentType = MediaTypeNames.Application.Json
            };
        }
    }
}
