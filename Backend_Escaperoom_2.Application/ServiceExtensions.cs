using AutoMapper;
using FluentValidation;
using Backend_Escaperoom_2.Application.Behaviours;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Mappings;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Backend_Escaperoom_2.Application.Features.EscapeTimer;

namespace Backend_Escaperoom_2.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<EscapeTimerManager>();
            services.AddSingleton<DataManager>();
            services.AddScoped<LanguagesHelper>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(provider.GetService<IDataProtectionProvider>()));
            }).CreateMapper());
        }
    }
}
