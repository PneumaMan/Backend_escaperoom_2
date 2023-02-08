using Backend_Escaperoom_2.Application;
using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Models.Hubs;
using Backend_Escaperoom_2.Infrastructure.Persistence;
using Backend_Escaperoom_2.Infrastruture.Shared;
using Backend_Escaperoom_2.WebApi.Extensions;
using Backend_Escaperoom_2.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Vereyon.Web;

namespace Backend_Escaperoom_2.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddCors(options =>
            {
                string[] urls = { "http://localhost:3000", "https://victorious-bay-049f02e10.2.azurestaticapps.net" };
                options.AddPolicy(
                  "CorsPolicy", x => x.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            });
            services.AddControllersWithViews();
            services.AddApplicationLayer();
            services.AddPersistenceInfrastructurePersistence(this.Configuration);
            services.AddSharedInfrastructure(this.Configuration);
            services.AddHealthChecks();
            services.AddSwaggerExtension();
            services.AddApiVersioningExtension();
            services.AddDataProtection();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddFlashMessage();
            services.AddHttpContextAccessor();

            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddTransient<IAppVersionService, AppVersionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            //app.UseCors(x => x.SetIsOriginAllowed(origin => true).AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseErrorHandlingMiddleware();
            app.UseSwaggerExtension();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<EscapeHub>("/times-escape");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
