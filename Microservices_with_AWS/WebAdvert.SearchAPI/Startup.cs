using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.SearchAPI.Extensions;
using WebAdvert.SearchAPI.Services;

namespace WebAdvert.SearchAPI
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
            services.AddControllers();

            // Calls extension method
            services.AddElasticSearch(Configuration);

            // DI
            services.AddTransient<ISearchService, SearchService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //Inject ILoggerFactory for logging
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            loggerFactory.AddAWSProvider(Configuration.GetAWSLoggingConfigSection(),
                formatter: (loglevel,message,exception) => $"{DateTime.UtcNow} {loglevel} {message} {exception?.StackTrace}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
