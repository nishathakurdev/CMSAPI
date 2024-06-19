using log4net.Config;
using log4net;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace ContactManagementSystemAPI
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4netconfig.config"));
            var demo = new Logger();
            demo.Error(exception.InnerException.ToString(),exception);

            context.Response.ContentType = "application/json";

            ResponseMessage _resp = new ResponseMessage
            {
                Data = null,
                Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                StatusCode = HttpStatusCode.InternalServerError,
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(_resp));
        }
    }
}
