using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace WillyNet.SGP.Presentation.WebApi.Extensions
{
    public static class ServicesExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //c.IncludeXmlComments(string.Format(@"{0}\CleanArchitecture.DemoAPI.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SGP - Sistema de Gestión de Proyectos API",
                    Description = "Sistema para aprobar o rechazar un proyecto pasando por diferentes módulos.",
                    Contact = new OpenApiContact
                    {
                        Name = "WillyNet",
                        Email = "willyrhcp96@gmail.com",
                        Url = new Uri("https://www.instagram.com/_willyvanilli/"),
                    }
                });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(confing =>
            {
                confing.DefaultApiVersion = new ApiVersion(1, 0);
                confing.AssumeDefaultVersionWhenUnspecified = true;
                confing.ReportApiVersions = true;
            });
        }
    }
}
