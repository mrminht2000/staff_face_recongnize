using StaffManagement.Core.Services.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                context.Response.ContentType = "application/json";
                var response = new ErrorRespond();

                switch (error)
                {
                    case UnauthorizedAccessException e:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case NullReferenceException e:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                response.Message = error.Message;
                response.Code = context.Response.StatusCode;
                await context.Response.WriteAsync(response.ToString(), Encoding.UTF8);
            }
        }
    }
}
