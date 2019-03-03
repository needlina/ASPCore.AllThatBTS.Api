using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (BaseException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, BaseException exception)
        {

            GlobalDiagnosticsContext.Set("configDir", Path.Combine(Directory.GetCurrentDirectory(), "NLogFile"));
            string logConfigPath = string.Empty;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                logConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.Development.config");
            }
            else
            {
                logConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.config");
            }

            var logger = NLogBuilder.ConfigureNLog(logConfigPath).GetCurrentClassLogger();

            logger.Log(LogLevel.Error, exception, exception.Description);

            Response response = new Response()
            {
                Message = exception.Description,
                ErrMsg = exception.Message,
                Status = exception.Code.ToString()
            };

            var result = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
