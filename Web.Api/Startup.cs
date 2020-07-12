using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using Web.Api.ActionFilters;
using Web.Api.Extensions;
using Web.Api.Utility;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isProduction = environment == Entities.Configuration.EnvironmentName.Production;
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), 
                isProduction ? "/nlog.production.config" : "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();

            services.AddAutoMapper(typeof(Startup));

            services.ConfigureEmailService(Configuration);
            services.ConfigureFrontend(Configuration);

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                //We added the ReturnHttpNotAcceptable = true option, which tells
                //the server that if the client tries to negotiate for the media type the
                //server doesn’t support, it should return the 406 Not Acceptable status
                //code.
                config.ReturnHttpNotAcceptable = true;
            })
                .AddNewtonsoftJson() // added for HttpPatch
                .AddXmlDataContractSerializerFormatters()
                .AddCustomCSVFormatter();

            //To return 422 instead of 400, the first thing we have to do is to suppress the BadRequest error when the ModelState is invalid.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidatePostExistsAttribute>();

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // added from book, what is that ?
                app.UseHsts();
            }

            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            // enables using static files for the request. If we don’t set a path to the static files directory, it will use a wwwroot folder in our project by default.
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            // will forward proxy headers to the current request. This will help us during application deployment.
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
