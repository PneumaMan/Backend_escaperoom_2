using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<Usuario> userManager, RoleManager<Role> roleManager,
            IDateTimeService dateTimeService)
        {
            //Seed Default User
            var id = Guid.NewGuid();
            var defaultUser = new Usuario
            {
                Id = id.ToString(),
                TipoLogueo = (int)TiposLogueo.Correo,
                UserName = "pneumaconsulting@gmail.com",
                Email = "pneumaconsulting@gmail.com",
                EstadoUsuario = true,
                ChangedPassword = false,
                Registered = dateTimeService.Now,
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin.1234*");
                    await userManager.AddToRoleAsync(defaultUser, TiposUsuarios.Desarrollador.ToString());
                    await userManager.AddToRoleAsync(defaultUser, TiposUsuarios.Administrador.ToString());
                    await userManager.AddToRoleAsync(defaultUser, TiposUsuarios.Participante.ToString());
                }

            }
        }
    }
}
