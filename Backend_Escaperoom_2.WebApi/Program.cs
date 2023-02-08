using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Domain.Entities;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using Backend_Escaperoom_2.Infrastructure.Persistence.Contexts;
using Azure.Identity;

namespace Backend_Escaperoom_2.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();

            //Initialize Logger
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    var dateTimeService = services.GetRequiredService<IDateTimeService>();

                    await Infrastructure.Persistence.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                    await Infrastructure.Persistence.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager, dateTimeService);

                    Log.Information("Datos predeterminados de inicialización finalizados.");
                }
                catch (Exception e)
                {
                    Log.Warning(e, "Se produjo un error al parametrizar la base de datos.");
                }
            }

            try
            {
                Log.Information("Inicio de la aplicación.");
                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, $"Se produjo un error al iniciar la aplicación: {e.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                /*.ConfigureAppConfiguration((context, config) =>
                {
                var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("APP_KEY_VAULT_ESCAPE_ROOM"));
                config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
                })*/
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
