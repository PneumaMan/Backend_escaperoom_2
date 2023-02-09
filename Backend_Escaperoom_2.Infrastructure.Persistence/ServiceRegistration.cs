using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using Backend_Escaperoom_2.Infrastructure.Persistence.Contexts;
using Backend_Escaperoom_2.Infrastructure.Persistence.Repositories;
using Backend_Escaperoom_2.Infrastructure.Persistence.Repositories.GenericRepository;
using Backend_Escaperoom_2.Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string cadenaConexion = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(cadenaConexion, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

            }, ServiceLifetime.Transient);

            /*Repositorios*/
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IUsuarioRepositoryAsync, UsuarioRepositoryAsync>();
            services.AddScoped<IRolesRepositoryAsync, RolesRepositoryAsync>();
            services.AddScoped<IEscapeRoomsRepositoryAsync, EscapeRoomsRepositoryAsync>();
            services.AddScoped<IParticipantesRepositoryAsync, ParticipantesRepositoryAsync>();
            services.AddScoped<ITipoParticipantesRepositoryAsync, TipoParticipantesRepositoryAsync>();
            services.AddScoped<ITeamsRepositoryAsync, TeamsRepositoryAsync>();
            //services.AddScoped<IRetosRepositoryAsync, RetosRepositoryAsync>();
            //services.AddScoped<IRespuestasRepositoryAsync, RespuestasRepositoryAsync>();
            //services.AddScoped<IRespuestasParticipantesRepositoryAsync, RespuestasParticipantesRepositoryAsync>();
            //services.AddScoped<IEncuestasRepositoryAsync, EncuestasRepositoryAsync>();

            /*Services*/
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRoleService, RoleService>();

            /*Identity y tokens*/
            services.AddIdentity<Usuario, Role>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.SignIn.RequireConfirmedEmail = false;
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = true;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = true;
                cfg.Password.RequireNonAlphanumeric = true;
                cfg.Password.RequireUppercase = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<DataProtectionTokenProviderOptions>(
                opt => opt.TokenLifespan = TimeSpan.FromHours(Double.Parse(configuration["JWTSettings:TokenProvidersDurationInHours"]))
            );

            /*Cookies*/
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(Double.Parse(configuration["JWTSettings:CookieExpireTimeInHours"]));
            });

            services.AddAuthentication(options =>
            {
                //SI LA WEB API SOLO MANEJA PETICIONES MEDIANTE TOKEN DESCOMENTAR LAS 3 LINEAS DE ABAJO
                //SI LA WEB API ES API Y WEB COMENTAR LAS 3 LINEAS DE ABAJO Y EN CADA CONTROLADOR QUE MANEJE PETICIONES CON TOKEN SE DEBEN DEFINIR EN EL CONTROLADOR
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(o =>
            {
                o.SaveToken = false;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };

                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        return Task.CompletedTask;
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        var message = c.HttpContext.RequestServices.GetRequiredService<LanguagesHelper>().TokenUnauthorized;
                        c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>()
                        {
                            Message = message,
                            Path = c.Request.Path
                        });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        var message = c.HttpContext.RequestServices.GetRequiredService<LanguagesHelper>().TokenOnForbidden;
                        c.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>()
                        {
                            Message = message,
                            Path = c.Request.Path
                        });
                        return c.Response.WriteAsync(result);
                    },
                };
            });
        }
    }
}
