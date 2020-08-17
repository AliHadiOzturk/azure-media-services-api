using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoAPI.app;
using VideoAPI.app.repositories;
using VideoAPI.app.repositories.implementation;
using VideoAPI.app.services;
using VideoAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VideoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var conString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(context => context.UseNpgsql(conString, o => o.MigrationsHistoryTable("_migrations", "public")));
            services.Configure<AzureConfiguration>(Configuration.GetSection("AzureConfiguration"));

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllers();
            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = long.MaxValue;
                x.ValueLengthLimit = int.MaxValue;
            });
            services.AddAutoMapper(typeof(MappingProfiles));

            //Add Services to DI
            services.AddTransient<AzureService>();
            services.AddTransient<JobService>();
            services.AddTransient<TransformService>();
            services.AddTransient<AssetService>();
            services.AddTransient<StreamLocatorService>();
            services.AddTransient<StreamingEndPointService>();

            services.AddTransient<ContentService>();
            services.AddTransient<ContentDomainService>();
            services.AddTransient<DocumentService>();

            //Add Repositories to DI
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IStreamingUrlRepository, StreamingUrlRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName.Equals(Environments.Development))
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOptions();
            app.UseJwt();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
