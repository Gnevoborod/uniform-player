using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using uniform_player.Controllers.Dtos.Common;

namespace uniform_player.Infrastructure.Exceptions
{
    public class ApiExceptionFilter:IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            var errorDto = new ErrorDto()
            {
                Code = exceptionContext.Exception.HResult.ToString(),
                Message = exceptionContext.Exception.Message,
            };
            exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            exceptionContext.Result = new JsonResult(errorDto);
        }
    }
}
