using Microsoft.AspNetCore.Builder;
using Stem.Extensions.MiddleWares;
using VideoAPI.Infrastructure.Middlewares;

namespace VideoAPI.Infrastructure
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseOptions(this IApplicationBuilder app) => app.UseMiddleware<OptionsMiddleware>();
        public static IApplicationBuilder UseJwt(this IApplicationBuilder app) => app.UseMiddleware<JWTMiddleware>();

        //public static IApplicationBuilder UseAuthenticationFilter(this IApplicationBuilder app) => app.UseMiddleware<AuthenticationFilter>();
    }
}