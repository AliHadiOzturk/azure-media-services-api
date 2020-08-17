using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VideoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                })
                .UseIISIntegration()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();
            });
        }
        // .ConfigureWebHostDefaults((hostingContext, config) =>
        //                               {
        //                                   // Call additional providers here as needed.
        //                                   // Call AddEnvironmentVariables last if you need to allow
        //                                   // environment variables to override values from other 
        //                                   // providers.
        //                                   config.AddEnvironmentVariables(prefix: "PREFIX_");
        //                               })

    }
}
