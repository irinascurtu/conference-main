using Conference.Api.Infrastructure;
using Conference.Data.Repositories;
using Conference.Domain;
using Conference.Logging;
using Conference.Logging.Data;
using Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Log4NetProvider = Conference.Logging.Log4NetProvider;

namespace Conference.Api
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

            services.AddDbContext<ConferenceContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConferenceDb"));
            });

            services.AddDbContext<LoggingDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LoggingDb"));
            });

            services.AddScoped<ISpeakerRepository, SpeakerRepository>();
            services.AddScoped<ITalkRepository, TalkRepository>();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddProvider(new DbLoggingProvider(GetLoggingDbContext(services)));
            });

           // builder.AddProvider(new Log4NetProvider("log4net.config"));
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ConferenceContext>().Database.EnsureCreated();
                    serviceScope.ServiceProvider.GetService<ConferenceContext>().EnsureSeeded();
                }

            }

            loggerFactory.AddLog4Net();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public LoggingDbContext GetLoggingDbContext(IServiceCollection services)
        {
            var builder = services.BuildServiceProvider();
            return builder.GetService<LoggingDbContext>();
        }
    }
}
