using System;
using Conference.Api.Infrastructure;
using Conference.Data.Repositories;
using Conference.Domain;
using Conference.Logging.Data;
using Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Conference.Api.Infrastructure.MappingProfiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

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

            services.AddControllers(options =>
                {
                  //  options.ReturnHttpNotAcceptable = true;
                    options.RespectBrowserAcceptHeader = true;
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "https://conference-api.com/modelvalidationproblem",
                            Title = "One or more model validation errors occurred.",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "See the errors property for details.",
                            Instance = context.HttpContext.Request.Path
                        };

                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                        //can be replaced with BadRequestObjectResult
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            // services.AddApiVersioning(o => o.ApiVersionReader = new UrlSegmentApiVersionReader());
             // services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));

            services.AddApiVersioning(o => o.ApiVersionReader = new MediaTypeApiVersionReader("v"));

            //services.AddApiVersioning(o => o.ApiVersionReader = 
            //    ApiVersionReader.Combine(new QueryStringApiVersionReader(),
            //        new HeaderApiVersionReader("api-version"),
            //    new MediaTypeApiVersionReader("v")));


            //defaults to api-version
            //services.AddApiVersioning( 
            //    options => options.ApiVersionReader = new QueryStringApiVersionReader());

            //?v=2.0
            //services.AddApiVersioning(
            //    options => options.ApiVersionReader = new QueryStringApiVersionReader("v"));

            #region Versioning Behavior

            //services.AddApiVersioning(o =>
            //{
            //    o.DefaultApiVersion = new ApiVersion(2, 0);
            //    o.ReportApiVersions = true;
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //    o.ErrorResponses = new ApiVersioningErrorProvider();
            //});

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
