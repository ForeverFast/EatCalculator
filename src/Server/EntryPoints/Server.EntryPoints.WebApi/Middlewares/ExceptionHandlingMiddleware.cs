using Common.Base.Exceptions;
using Common.Exceptions;
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
            => ex switch
            {
                NotFoundException notFoundException => HandleException(context, notFoundException),
                BadRequestException badRequestException => HandleException(context, badRequestException),
                ForbiddenException forbiddenException => HandleException(context, forbiddenException),
                UnauthorizedException unauthorizedException => HandleException(context, unauthorizedException),
                InternalServerErrorException internalServerErrorException => HandleException(context, internalServerErrorException),
                _ => HandleException(context, new InternalServerErrorException(ex.Message)),
            };

        private static Task HandleException<T>(HttpContext context, BaseException<T> apiException) where T : BaseExceptionModel
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)apiException.StatusCode;
            var json = JsonSerializer.Serialize(apiException.ErrorModel,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return context.Response.WriteAsync(json);
        }
    }
}
