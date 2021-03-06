﻿using Contracts;
using EmailService;
using Entities;
using Entities.Configuration;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Utility;

namespace Web.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    //.WithOrigins("https://www.examples.com")
                    .AllowAnyMethod()
                    //.WithMethods("POST", "GET")
                    .AllowAnyHeader()
                    //.WithHeaders("accept", "context-type")
                    );
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => { });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
                    b.MigrationsAssembly("Web.Api")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 5;
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            //var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SN_JWTSETTINGS_SECRETKEY");

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Environment.GetEnvironmentVariable("SN_JWTSETTINGS_VALIDISSUER"),
                        ValidAudience = Environment.GetEnvironmentVariable("SN_JWTSETTINGS_VALIDAUDIENCE"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
        }

        public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration) {
            var emailConfig = new EmailConfiguration() {
                From = Environment.GetEnvironmentVariable("SN_EMAILCONFIG_FROM"),
                SendGridApiKey = Environment.GetEnvironmentVariable("SN_EMAILCONFIG_SENDGRID_APIKEY")
            };
            services.AddSingleton(emailConfig);
        }

        public static void ConfigureCloudinary(this IServiceCollection services, IConfiguration configuration) {
            var config = new CloudinarySettings() {
                CloudName = Environment.GetEnvironmentVariable("SN_CLOUDINARY_CLOUDNAME"),
                ApiKey = Environment.GetEnvironmentVariable("SN_CLOUDINARY_APIKEY"),
                ApiSecret = Environment.GetEnvironmentVariable("SN_CLOUDINARY_APISECRET")
            };
            services.AddSingleton(config);
        }

        public static void ConfigureFrontend(this IServiceCollection services, IConfiguration configuration) {
            var frontendConfiguration = new FrontendConfiguration() {
                AuthenticationControllerName = Environment.GetEnvironmentVariable("SN_FRONTENDSETTINGS_AUTHCONTROLLERNAME"),
                BaseUrl = Environment.GetEnvironmentVariable("SN_FRONTENDSETTINGS_BASEURL"),
                ForgotPasswordActionName = Environment.GetEnvironmentVariable("SN_FRONTENDSETTINGS_FORGOTPASSWORDACTIONNAME"),
                RegisterConfirm = Environment.GetEnvironmentVariable("SN_FRONTENDSETTINGS_REGISTRATIONCONFIRM"),
            };
            services.AddSingleton(frontendConfiguration);
        }

        public static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<RepositoryContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

    }
}
