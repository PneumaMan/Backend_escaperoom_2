using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Domain.Settings;
using Backend_Escaperoom_2.Infrastruture.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend_Escaperoom_2.Infrastruture.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<URLSettings>(config.GetSection(nameof(URLSettings)));
            services.Configure<JWTSettings>(config.GetSection(nameof(JWTSettings)));
            services.Configure<LanguageSettings>(config.GetSection(nameof(LanguageSettings)));
            services.Configure<PaginationSettings>(config.GetSection(nameof(PaginationSettings)));

            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IUrlServices, UrlServices>();
        }
    }
}
