using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Stem.Extensions.MiddleWares
{
    public class OptionsMiddleware
    {
        private readonly RequestDelegate next;
        private IWebHostEnvironment environment;
        public OptionsMiddleware(RequestDelegate _next, IWebHostEnvironment _environment)
        {
            next = _next;
            environment = _environment;
        }

        public async Task Invoke(HttpContext context)
        {
            this.BeginInvoke(context);
            if (context.Request.Method != "OPTIONS")
                await next.Invoke(context);
        }

        private void BeginInvoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)context.Request.Headers["Origin"] });
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept,Authorization" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                context.Response.StatusCode = 200;
                context.Response.WriteAsync("OK");
                return;
            }
        }
    }
}