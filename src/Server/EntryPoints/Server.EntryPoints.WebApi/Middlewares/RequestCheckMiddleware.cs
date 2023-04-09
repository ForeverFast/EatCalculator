using Common.Helpers;
using System.Security.Claims;

namespace Server.EntryPoints.WebApi.Middlewares
{
    public class RequestCheckMiddleware
    {
        #region Injects

        private readonly RequestDelegate _next;

        #endregion

        #region Ctors

        public RequestCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split().Last();
            if (token == null)
            {
                await _next(context);
                return;
            }

            var claims = IdentityHelper.ParseClaimsFromJwt(token);
            var userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            context.Request.Headers.Add("AuthorizedUserId", userId!.Value.ToString());

            await _next(context);
        }
    }
}
