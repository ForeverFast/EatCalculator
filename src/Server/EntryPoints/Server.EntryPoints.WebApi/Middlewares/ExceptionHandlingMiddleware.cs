using System.Net;
using System.Text.Json;

namespace Server.EntryPoints.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        #region Injects

        private readonly RequestDelegate _next;

        #endregion

        #region Ctors

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var json = JsonSerializer.Serialize(ex.Message,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return context.Response.WriteAsync(json);
        }
    }
}
