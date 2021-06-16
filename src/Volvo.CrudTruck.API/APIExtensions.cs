using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volvo.CrudTruck.API.Helpers;
using Volvo.CrudTruck.Application.Auth;
using Volvo.CrudTruck.Application.Interfaces;
using Volvo.CrudTruck.Application.Services;
using Volvo.CrudTruck.Domain.Repository;

namespace Volvo.CrudTruck.API
{
    public static class APIExtensions
    {

        /// <summary>
        /// Configure JWT Authentication with configurations in app.settings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurations = new TokenConfigurations();
            var signInConfigurations = new SignInConfigurations();
            services.AddSingleton(signInConfigurations);
            services.AddSingleton(tokenConfigurations);

            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, bearerOptions =>
                   {
                       var paramsValidation = bearerOptions.TokenValidationParameters;
                       paramsValidation.IssuerSigningKey = signInConfigurations.Key;
                       paramsValidation.ValidAudience = tokenConfigurations.Audience;
                       paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                       paramsValidation.ValidateIssuerSigningKey = true;
                       paramsValidation.ValidateLifetime = true;
                       paramsValidation.ClockSkew = TimeSpan.Zero;
                   });
        }

        /// <summary>
        /// Configure swagger dashboard visualization to API
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Write 'Bearer ' + token' without inverted commas",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Volvo .NET Test ",
                    Description = "Truck registry crud",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Renan Kohl",
                        Url = new Uri("https://github.com/RenanKohl/CrudTruck/"),
                        Email = "renan.kohl@hotmail.com"
                    }
                });

                c.OperationFilter<AuthResponsesOperationFilter>();

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(i => i.FullName);

            });

        }

        /// <summary>
        /// Add dependency injection 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterDependencies(this IServiceCollection services)
        {
            //services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<SeedingService>();

        }

    }
}

