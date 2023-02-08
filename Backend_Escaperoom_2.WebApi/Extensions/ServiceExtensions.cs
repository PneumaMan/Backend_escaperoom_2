using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Backend_Escaperoom_2.Application.Assets.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Backend_Escaperoom_2.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            string version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<SwaggerIgnoreFilter>();
                c.SwaggerDoc($"v{version}", new OpenApiInfo
                {
                    Version = $"v{version}",
                    Title = "",
                    Description = "Esta API será responsable de la parte logica del escape room.",
                    Contact = new OpenApiContact
                    {
                        Name = "Pneuma Consulting S.A",
                        Email = "andres.galindo@pneumaconsulting.com",
                        //Url = new Uri("https://www.pneumaconsulting.co/contactenos"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Autorización",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Ingrese su Bearer token en este formato: Bearer {Ingrese tu token aquí}, para acceder a esta API."
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
                //c.IncludeXmlComments(string.Format(@"{0}\Backend_Escaperoom_2.WebApi.xml", AppDomain.CurrentDomain.BaseDirectory));
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            string version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            string[] versionNumber = version.Split('.');

            services.AddApiVersioning(config =>
            {
                //Especifique la versión de API predeterminada como 1.0
                config.DefaultApiVersion = new ApiVersion(Int32.Parse(versionNumber[0]), Int32.Parse(versionNumber[1]));

                //Si el cliente no ha especificado la versión de API en la solicitud, use el número de versión de API predeterminado
                config.AssumeDefaultVersionWhenUnspecified = true;

                //Anuncie las versiones de API compatibles con el punto final en particular
                config.ReportApiVersions = true;
            });
        }
    }
}
