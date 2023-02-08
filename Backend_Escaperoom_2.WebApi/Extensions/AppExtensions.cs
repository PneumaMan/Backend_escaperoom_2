using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Backend_Escaperoom_2.WebApi.Middlewares;
using Backend_Escaperoom_2.Application.Interfaces;

namespace Backend_Escaperoom_2.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var version = app.ApplicationServices.GetRequiredService<IAppVersionService>();
                string[] versionNumber = version.Version.Split('.');
                options.SwaggerEndpoint($"/swagger/v{version.Version}/swagger.json", $"Escape Room - v{version.Version}");
            });
        }

        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
