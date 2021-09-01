using Billdeer.Core.Extensions;
using Billdeer.Core.Utilities.Messages;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";
            if (e.GetType().Assembly == typeof(ValidationException).Assembly)
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(ApplicationException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            //else if (e.GetType() == typeof(UnauthorizedAccessException))
            //{
            //    message = e.Message;
            //    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //}
            //else if (e.GetType() == typeof(SecurityException))
            //{
            //    message = e.Message;
            //    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //}
            //else
            //{
            //    message = ExceptionMessage.InternalServerError;
            //}

            //await httpContext.Response.WriteAsync(message);
            await httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }

    }
}
