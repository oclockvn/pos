using Microsoft.AspNetCore.Diagnostics;
using pos.core;
using pos.core.Exceptions;

namespace pos.web.Middlewares
{
    public static class ExceptionHandlerExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this WebApplication app)
        {
            return app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var exceptionContext =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    var (errorCode, msg) = GetStatusCode(exceptionContext.Error);

                    //var exceptionResult = new Result<bool>(errorCode);

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        statusCode = errorCode.ToString(),
                        msg,
                    });
                });
            });
        }

        private static (StatusCode, string) GetStatusCode(Exception ex)
        {
            if (ex is BusinessException businessEx)
            {
                return (businessEx.StatusCode, businessEx.Message);
            }

            if (ex is ArgumentException || ex is ArgumentNullException)
            {
                return (StatusCode.Argument_exception, ex.Message);
            }

            return (StatusCode.Internal_server_error, ex.Message);
        }
    }
}
