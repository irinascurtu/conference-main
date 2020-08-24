using System;
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
using AutoMapper;
using Conference.Api.Infrastructure.MappingProfiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json")
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ConferenceContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConferenceDb"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(TalkProfile));

            services.AddScoped<ISpeakerRepository, SpeakerRepository>();
            services.AddScoped<ITalkRepository, TalkRepository>();

            services.AddControllers();
            //services.AddControllers(options => { options.ReturnHttpNotAcceptable = true; });
            //  .AddXmlDataContractSerializerFormatters();

            #region ApiBehavior
            //services.AddControllers()
            //    .ConfigureApiBehaviorOptions(setupAction =>
            //    {
            //        setupAction.InvalidModelStateResponseFactory = context =>
            //        {
            //            var problemDetails = new ValidationProblemDetails(context.ModelState)
            //            {
            //                Type = "https://conference-api.com/modelvalidationproblem",
            //                Title = "One or more model validation errors occurred.",
            //                Status = StatusCodes.Status422UnprocessableEntity,
            //                Detail = "See the errors property for details.",
            //                Instance = context.HttpContext.Request.Path
            //            };

            //            problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
            //can be replaced with BadRequestObjectResult
            //            return new UnprocessableEntityObjectResult(problemDetails)
            //            {
            //                ContentTypes = { "application/problem+json" }
            //            };
            //        };
            //    });
            #endregion
            #region others

            //services.AddDbContext<LoggingDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("LoggingDb"));
            //});
            //    .AddJsonOptions(option =>
            //{
            //    option.JsonSerializerOptions.PropertyNamingPolicy = null;
            //    option.JsonSerializerOptions.MaxDepth = 256;
            //}); ;

            #endregion
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

            app.UseRouting();

            //  app.UseAuthorization();

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
