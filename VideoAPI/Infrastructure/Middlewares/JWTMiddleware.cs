using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace VideoAPI.Infrastructure.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate next;
        private IWebHostEnvironment environment;
        public JWTMiddleware(RequestDelegate _next, IWebHostEnvironment _environment)
        {
            next = _next;
            environment = _environment;
        }

        public async Task Invoke(HttpContext context)
        {
            System.Console.WriteLine("-----------------------------");
            System.Console.WriteLine("LOCAL ADDRESS:" + context.Request.Path.Value);
            System.Console.WriteLine("HTTP METHOD:" + context.Request.Method);
            System.Console.WriteLine("CONTENT-TYPE:" + context.Request.ContentType);
            System.Console.WriteLine("QUERY STRING:" + context.Request.QueryString);
            System.Console.WriteLine(DateTime.Now);
            await next.Invoke(context);
        }



    }
}